using KitapeviStokTakipWebApi.Helpers;
using KitapeviStokTakipWebApi.IServices;
using KitapeviStokTakipWebApi.Models;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Oracle.ManagedDataAccess.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace KitapeviStokTakipWebApi.Services
{
    public class AuthService : IAuthService
    {
        private readonly string _connectionString;
        private readonly AppSettings _appSettings;

        public AuthService(IConfiguration configuration, IOptions<AppSettings> appSettings)
        {
            _connectionString = configuration.GetConnectionString("OracleDbConnection");
            _appSettings = appSettings.Value;
        }
        public string RegisterUser(User user)
        {
            int userId = 0;

            using (OracleConnection connection = new OracleConnection(_connectionString))
            {
                using (OracleCommand command = new OracleCommand("SP_REGISTER_USER"))
                {
                    connection.Open();
                    using (OracleDataAdapter dataAdapter = new OracleDataAdapter())
                    {
                        command.CommandType = CommandType.StoredProcedure;

                        OracleParameter[] prm = new OracleParameter[3];

                        prm[0] = command.Parameters.Add("v_Username", OracleDbType.Varchar2,
                           user.Username, ParameterDirection.Input);
                        prm[1] = command.Parameters.Add("v_Password", OracleDbType.Varchar2,
                           user.Password, ParameterDirection.Input);
                        prm[2] = command.Parameters.Add("v_Email", OracleDbType.Varchar2,
                           user.Email, ParameterDirection.Input);
                        command.Connection = connection;

                        userId = Convert.ToInt32(command.ExecuteScalar());
                        connection.Close();
                    }
                }
                string message = string.Empty;
                switch (userId)
                {
                    case -1:
                        message = "Kullanıcı adı: " + user.Username + " zaten kullanılıyor. Lütfen başka bir tane deneyiniz";
                        break;
                    case -2:
                        message = "Email zaten mevcut: " + user.Email;
                        break;
                    default:
                        message = "Kayıt başarılı " + user.Username + " " + user.Email;
                        break;
                }
                return message;
            }
        }


        public IEnumerable<User> GetAllUsers()
        {
            List<User> users = new List<User>();
            try
            {
                using (OracleConnection connection = new OracleConnection(_connectionString))
                {
                    using (OracleCommand command = connection.CreateCommand())
                    {
                        connection.Open();

                        command.BindByName = true;
                        command.CommandText = "Select * From Members";

                        OracleDataReader reader = command.ExecuteReader();

                        while (reader.Read())
                        {
                            User u = new User
                            {
                                UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                                Username = reader["USER_NAME"].ToString(),
                                Password = reader["PASSWORD"].ToString()
                            };
            
                            users.Add(u);
                        }
                    }
                }
            }
            catch
            {
                throw;
            }

            return users;
        }

        private List<User> _users = new List<User>();

        public async Task<User> Authenticate(string username, string password)
        {
            using (OracleConnection connection = new OracleConnection("User Id =SYSTEM; Password =root;Data Source=localhost:1521/XE"))
            {
                using (OracleCommand command = connection.CreateCommand())
                {

                    connection.Open();

                    command.BindByName = true;
                    command.CommandText = "Select * From MEMBERS";

                    OracleDataReader reader = command.ExecuteReader();

                    while (reader.Read())
                    {
                        User _user = new User()
                        {
                            UserId = Convert.ToInt32(reader["MEMBER_ID"]),
                            Username = reader["USER_NAME"].ToString(),
                            Password = reader["PASSWORD"].ToString(),
                            Email = reader["EMAIL"].ToString(),
                            Role = reader["ROLE"].ToString()
                        };

                        _users.Add(_user);
                    }

                }
            }

            var user = await Task.Run(() => _users.SingleOrDefault(x => x.Username == username && x.Password == password));

            if (user == null)
                return null;

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, user.UserId.ToString()),
                    new Claim(ClaimTypes.Role, user.Role)
                }),
                Expires = DateTime.UtcNow.AddDays(7),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            user.Token = tokenHandler.WriteToken(token);

            return user.WithoutPassword();
        }

    }
}


