using Demo.BLL.InterFaces;
using Demo.DAL.Contexts;
using Demo.DAL.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Demo.BLL.Repositories
{
    public class EmployeeRepository :GenericRepository<Employee> , IEmployeeRepository
    {
        public EmployeeRepository(MVCExceedDBContext dBContext ):base(dBContext) {
        
        }
       
    }
}
