using System.ComponentModel.DataAnnotations.Schema;

namespace api_poc.Models
{
    public class User
    {
        #region Properties

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }

        #endregion

        #region Constructors

        public User()
        {

        }

        public User(string name, string email)
        {
            this.Name = name;
            this.Email = email;
        }

        #endregion
    }
}
