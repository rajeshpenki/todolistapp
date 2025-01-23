using System.Data;
using System.Threading.Tasks;
using ToDoList.Server.Business;

namespace ToDoList.Server.Model
{
    public class ToDoListDBContext
    {

        private List<ToDoTask> DbDataSet { get; set; }

        public ToDoListDBContext() {

            DbDataSet =  new List<ToDoTask>();
        }


        public List<ToDoTask> GetTasks()
        {
            return DbDataSet;
        }

        public bool AddTask(ToDoTask task)
        {
            bool retunrvalue = false;
            try
            {
                if (DbDataSet.Where(item => item.Id == task.Id).Count()== 0)
                {
                    var maxvalue = DbDataSet.OrderByDescending(item => item.Id).FirstOrDefault();
                    var id = maxvalue != null ? maxvalue.Id + 1 : 1;
                    task.Id = id;
                    DbDataSet.Add(task);
                    retunrvalue = true;
                }
            }
            catch(Exception ex)
            {
                retunrvalue = false;
            }

            return retunrvalue;
        }

        public bool DeleteTask(int id)
        {
            bool retunrvalue = false;
            try
            {
                var item = DbDataSet.Where(item => item.Id == id).FirstOrDefault();
                if (item != null)
                {
                    DbDataSet.Remove(item);

                    retunrvalue = true;
                }
            }
            catch (Exception ex)
            {
                retunrvalue = false;
            }

            return retunrvalue;
        }


        public bool UpdateTask(ToDoTask task)
        {
            bool retunrvalue = false;
            try
            {
                var item = DbDataSet.Where(item => item.Id == task.Id).FirstOrDefault();
                if (item != null)
                {
                    item.Completed = task.Completed;
                    item.Name = task.Name;
                    retunrvalue = true;
                }
            }
            catch (Exception ex)
            {
                retunrvalue = false;
            }

            return retunrvalue;
        }
    }
}
