import { Injectable } from '@angular/core';

import { HttpClient, HttpParams } from '@angular/common/http';
import { map } from 'rxjs/operators';

import { Aluno } from '../models/Aluno';

import { environment } from 'src/environments/environment';
import { PaginatedResult } from '../models/Pagination';
import { Observable } from 'rxjs';


@Injectable({
  providedIn: 'root'
})
export class AlunoService {

  baseURL = `${environment.mainUrlAPI}aluno`;

  constructor(private http: HttpClient){}

  getAll(page?: number, pageItems?: number): Observable<PaginatedResult<Aluno[]>> {

    const paginatedResult: PaginatedResult<Aluno[]> = new PaginatedResult<Aluno[]>();

    // console.log('Resultado paginatedResult Resultado', paginatedResult)

    let params = new HttpParams();

    if (page != null && pageItems != null) {
      params = params.append('pageNumber', page.toString());
      params = params.append('pageSize', pageItems.toString());
    }

    return this.http.get<Aluno[]>(this.baseURL, { observe: 'response', params })
      .pipe(
        map(response => {
          // Assegure que result não é null
          paginatedResult.result = response.body ?? [];

          // console.log('Resultado paginatedResul', paginatedResult.result)

          const paginationHeader = response.headers.get('Pagination');

          // console.log('Resultado paginationHeader Resultado', paginationHeader)
          // Verifica se o cabeçalho não é null antes de chamar JSON.parse
          if (paginationHeader != null) {
            paginatedResult.pagination = JSON.parse(paginationHeader);
          }

          return paginatedResult;
        })
      );
  }


  getById(id: number): Observable<Aluno> {
    return this.http.get<Aluno>(`${this.baseURL}/${id}`);
  }

  getByDisciplinaId(id: number): Observable<Aluno[]> {
    return this.http.get<Aluno[]>(`${this.baseURL}/ByDisciplina/${id}`);
  }

  post(aluno: Aluno) {
    return this.http.post(this.baseURL, aluno);
  }

  put(aluno: Aluno) {
    return this.http.put(`${this.baseURL}/${aluno.id}`, aluno);
  }

  patch(aluno: Aluno) {
    return this.http.patch(`${this.baseURL}/${aluno.id}`, aluno);
  }

  trocarEstado(alunoId: number, ativo: boolean ) {
    return this.http.patch(`${this.baseURL}/${alunoId}/trocarEstado`, { estado: ativo});
  }

  delete(id: number) {
    return this.http.delete(`${this.baseURL}/${id}`);
  }
}
