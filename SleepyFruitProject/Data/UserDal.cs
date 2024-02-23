using SleepyFruitProject.Models;

namespace SleepyFruitProject.Data
{
    public class UserDal
    {
        private AppDbContext db;
        public UserDal(AppDbContext indb)
        {
            db = indb;
        }

        public User GetUser(string? id)
        {
            User? foundUser = db.OurUsers.Where(u => u.UserID == id).FirstOrDefault();
            return foundUser;
        }

        public void AddUser(User user)
        {
            db.OurUsers.Add(user);
            db.SaveChanges();
        }

        public void UpdateUser(User user)
        {
            db.OurUsers.Update(user);
            db.SaveChanges();
        }
    }
}
