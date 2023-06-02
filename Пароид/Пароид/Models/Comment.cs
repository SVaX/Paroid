using System;
using System.Collections.Generic;
using System.Data.SqlClient;

namespace Пароид.Models
{
    public partial class Comment
    {
        public int CommentId { get; set; }

        public int IdUser { get; set; }

        public int IdApp { get; set; }

        public DateTime Date { get; set; }

        public string CommentText { get; set; } = null!;

        public int Score { get; set; }

        public bool IsReported { get; set; }

        public string UserName
        {
            get
            {
                string connectionString = "Data Source=192.168.1.69\\SQLEXPRESS;Initial Catalog=Diplom; User=sa; Password = 123; Trusted_Connection = False";
                string databaseTable = "[User]";
                string selectQuery = String.Format("SELECT * FROM {0} WHERE UserId={1}", databaseTable, IdUser);
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    //open connection
                    connection.Open();

                    SqlCommand command = new SqlCommand(selectQuery, connection);

                    command.Connection = connection;
                    command.CommandText = selectQuery;
                    var result = command.ExecuteReader();
                    while (result.Read())
                    {
                        var user = new User();
                        user.UserId = int.Parse(result[0].ToString());
                        user.Login = result[1].ToString();
                        user.Password = result[2].ToString();
                        user.Balance = int.Parse(result[3].ToString());
                        user.PermissionLevel = result[4].ToString();
                        user.Email = result[5].ToString();
                        user.RegistrationDate = DateTime.Parse(result[6].ToString());
                        user.Description = result[7].ToString();
                        return user.Login;
                    }
                    return null;
                }
            }
        }

        public virtual Application IdAppNavigation { get; set; } = null!;

        public virtual User IdUserNavigation { get; set; } = null!;
    }
}
