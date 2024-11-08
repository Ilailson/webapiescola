import { Component, OnInit, TemplateRef, OnDestroy } from '@angular/core';
import { Aluno } from '../../models/Aluno';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { ToastrService } from 'ngx-toastr';
import { NgxSpinnerService } from 'ngx-spinner';
import { AlunoService } from '../../services/aluno.service';
import { takeUntil } from 'rxjs/operators';
import { Subject } from 'rxjs';
import { ProfessorService } from '../../services/professor.service';
import { Professor } from '../../models/Professor';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-alunos',
  templateUrl: './alunos.component.html',
  styleUrls: ['./alunos.component.css']
})
export class AlunosComponent implements OnInit, OnDestroy {

  public modalRef!: BsModalRef;
  public alunoForm!: FormGroup;
  public titulo = 'Alunos';
  public alunoSelectional!: Aluno;
  public textSimple!: string;
  public profsAlunos!: Professor[];

  private unsubscriber = new Subject();

  public alunos: Aluno[] = [];
  public aluno!: Aluno;
  public msnDeleteAluno!: string;
  public modeSave = 'post';

  constructor(
    private alunoService: AlunoService,
    private route: ActivatedRoute,
    private professorService: ProfessorService,
    private fb: FormBuilder,
    private modalService: BsModalService,
    private toastr: ToastrService,
    private spinner: NgxSpinnerService

  ) {
    this.criarForm();
  }

  openModal(template: TemplateRef<any>, alunoId: number): void {
    this.professoresAlunos(template, alunoId);
  }

  closeModal(): void {
    this.modalRef.hide();
  }

  professoresAlunos(template: TemplateRef<any>, id: number): void {
    this.spinner.show();
    this.professorService.getByAlunoId(id)
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((professores: Professor[]) => {
        this.profsAlunos = professores;
        this.modalRef = this.modalService.show(template);
      }, (error: any) => {
        this.toastr.error(`erro: ${error.message}`);
        console.error(error.message);
        this.spinner.hide()
      }, () => this.spinner.hide()
    );
  }


  ngOnInit(): void {
    this.carregarAlunos();
  }

  ngOnDestroy(): void {
    this.unsubscriber.next(null);
    this.unsubscriber.complete();
  }

  criarForm(): void {
    this.alunoForm = this.fb.group({
      id: [0],
      nome: ['', Validators.required],
      sobrenome: ['', Validators.required],
      telefone: ['', Validators.required]
    });
  }

  

  // saveAluno(): void {
  //   if (this.alunoForm.valid) {
  //     this.spinner.show();

  //     if (this.modeSave === 'post') {
  //       this.aluno = {...this.alunoForm.value};
  //     } else {
  //       this.aluno = {id: this.alunoSelectional.id, ...this.alunoForm.value};
  //     }

  //     this.alunoService[this.modeSave](this.aluno)
  //       .pipe(takeUntil(this.unsubscriber))
  //       .subscribe(
  //         () => {
  //           this.carregarAlunos();
  //           this.toastr.success('Aluno salvo com sucesso!');
  //         }, (error: any) => {
  //           this.toastr.error(`Erro: Aluno não pode ser salvo!`);
  //           console.error(error);
  //         }, () => this.spinner.hide()
  //       );

  //   }
  // }

  saveAluno(): void {
    if (this.alunoForm.valid) {
      this.spinner.show();
  
      if (this.modeSave === 'post') {
        this.aluno = {...this.alunoForm.value};
        this.alunoService.post(this.aluno)
          .pipe(takeUntil(this.unsubscriber))
          .subscribe(
            () => {
              this.carregarAlunos();
              this.toastr.success('Aluno salvo com sucesso!');
            }, (error: any) => {
              this.toastr.error(`Erro: Aluno não pode ser salvo!`);
              console.error(error);
            }, () => this.spinner.hide()
          );
      } else {
        this.aluno = {id: this.alunoSelectional.id, ...this.alunoForm.value};
        this.alunoService.put(this.aluno)
          .pipe(takeUntil(this.unsubscriber))
          .subscribe(
            () => {
              this.carregarAlunos();
              this.toastr.success('Aluno salvo com sucesso!');
            }, (error: any) => {
              this.toastr.error(`Erro: Aluno não pode ser salvo!`);
              console.error(error);
              this.spinner.hide()
            }, () => this.spinner.hide()
          );
      }
    }
  }
  

  // carregarAlunos(): void {
  //   const id = +this.route.snapshot.paramMap.get('id');

  //   this.spinner.show();
  //   this.alunoService.getAll()
  //     .pipe(takeUntil(this.unsubscriber))
  //     .subscribe((alunos: Aluno[]) => {
  //       this.alunos = alunos;

  //       if (id > 0) {
  //         this.alunoSelect(this.alunos.find(aluno => aluno.id === id));
  //       }

  //       this.toastr.success('Alunos foram carregado com Sucesso!');
  //     }, (error: any) => {
  //       this.toastr.error('Alunos não carregados!');
  //       console.log(error);
  //     }, () => this.spinner.hide()
  //   );
  // }

  carregarAlunos(): void {
    const idParam = this.route.snapshot.paramMap.get('id');
    const alunoId = idParam !== null ? +idParam : 0;
  
    this.spinner.show();
    this.alunoService.getAll()
      .pipe(takeUntil(this.unsubscriber))
      .subscribe((alunos: Aluno[]) => {
        this.alunos = alunos;
  
        if (alunoId > 0) {
          const alunoEncontrado = this.alunos.find(aluno => aluno.id === alunoId);
          if (alunoEncontrado) {
            this.alunoSelect(alunoEncontrado.id);
          }
        }
  
        this.toastr.success('Alunos foram carregados com sucesso!');
      }, (error: any) => {
        this.toastr.error('Alunos não carregados!');
        console.log(error);
        this.spinner.hide()
      }, () => this.spinner.hide()
    );
  }
  

  alunoSelect(alunoId: number): void {
    this.modeSave = 'patch';
    this.alunoService.getById(alunoId).subscribe(
      (alunoReturn) => {
        this.alunoSelectional = alunoReturn;
        this.alunoForm.patchValue(this.alunoSelectional);
      },
      (error) => {
        this.toastr.error('Alunos não carregados!');
        console.log(error);
        this.spinner.hide() 
      },
      () => this.spinner.hide()
    );
    
  }

  voltar(): void {
    this.alunoSelectional!;
  }

}
