namespace SleepyFruitProject.Data
{
    public class UserDal
    {
        public User GetUser(int? id)
        {
            //Movie? foundMovie = MovieList.Where(m => m.Id == id).FirstOrDefault();
            User? foundMovie = db.Movies.Where(m => m.Id == id).Include(m => m.Genre).FirstOrDefault();
            return foundMovie;

        }
    }
}
