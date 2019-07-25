using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using DataAccessLayer;
using DataAccessLayer.Entities;

namespace TMWcfService
{
    internal class Service : IService
    {
        private DataManager Dm => DataManager.Instance;

        public int AddNewDepartment(Department department)
        {
            try
            {
                return Dm.Department.Add(department);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public int AddNewWorker(Worker worker)
        {
            try
            {
                return Dm.Worker.Add(worker);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void DeleteDepartment(int id)
        {
            try
            {
                Dm.Department.Delete(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void DeleteWorker(int id)
        {
            try
            {
                Dm.Worker.Delete(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void EditDepartment(Department department)
        {
            try
            {
                Dm.Department.Update(department);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public void EditWorker(Worker worker)
        {
            try
            {
                Dm.Worker.Update(worker);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<Department> GetAllDepartments()
        {
            try
            {
                return Dm.Department.GetList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public List<Worker> GetAllWorkers()
        {
            try
            {
                return Dm.Worker.GetList();
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Department GetDepartmentById(int id)
        {
            try
            {
                return Dm.Department.GetItem(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }

        public Worker GetWorkerById(int id)
        {
            try
            {
                return Dm.Worker.GetItem(id);
            }
            catch (Exception ex)
            {
                throw new FaultException(ex.Message);
            }
        }
    }
}
