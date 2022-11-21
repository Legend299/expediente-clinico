using webservice1.Controllers;
using webservice1.Models;

namespace WebService1Test
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void LoginToRestApi()
        {
            var testUsers = GetTestUsers(); 
            var controller = new UsuarioController(testUsers);
            var result = controller.Login;
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void GetAllUsers_from_Db() {
            var controller = new UsuarioController();
            var result = controller.GetListaUsuarios();
            Assert.IsNotNull(result);
        }

        private List<Usuario> GetTestUsers()
        {
            var userList = new List<Usuario>();
            userList.Add(new Usuario { IdUsuario = 1, Correo = "usuario1@gmail.com", Password = "123456", IdRol = 3, IdExpediente = 1, Activo = true });
            userList.Add(new Usuario { IdUsuario = 2, Correo = "usuario2@gmail.com", Password = "123456", IdRol = 3, IdExpediente = 2, Activo = true });
            userList.Add(new Usuario { IdUsuario = 3, Correo = "usuario3@gmail.com", Password = "123456", IdRol = 3, IdExpediente = 3, Activo = true });
            userList.Add(new Usuario { IdUsuario = 4, Correo = "usuario4@gmail.com", Password = "123456", IdRol = 3, IdExpediente = 4, Activo = true });
            return userList;
        }
    }
}