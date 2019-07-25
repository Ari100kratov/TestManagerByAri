using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using DataAccessLayer.Entities;

namespace TMWcfService
{
    [ServiceContract]
    internal interface IService
    {
        [OperationContract]
        int AddNewWorker(Worker worker);

        [OperationContract]
        void EditWorker(Worker worker);

        [OperationContract]
        void DeleteWorker(int id);

        [OperationContract]
        int AddNewDepartment(Department department);

        [OperationContract]
        void EditDepartment(Department department);

        [OperationContract]
        void DeleteDepartment(int id);

        [OperationContract]
        List<Worker> GetAllWorkers();

        [OperationContract]
        Worker GetWorkerById(int id);

        [OperationContract]
        List<Department> GetAllDepartments();

        [OperationContract]
        Department GetDepartmentById(int id);
    }
}
