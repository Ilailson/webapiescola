using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SmartSchool.WebAPI.Data
{
    public interface IRepository
    {
        void Add<T>(T entity) where T : class; //receber tipo... Sera sempre classe
        void Update<T>(T entity) where T : class; //receber tipo... Sera sempre classe
        void Delete<T>(T entity) where T : class; //receber tipo... Sera sempre classe
        bool SaveChanges();
    }
}