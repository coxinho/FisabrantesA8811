using System;
using Fisabrantes.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(Fisabrantes.Startup))]
namespace Fisabrantes
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            // inicia os utilizadores
            iniciaAplicacao();
        }

        // Criar Roles e Utilizadores
        // Em Startup iam criando a primeira função admin e criar um usuário Administrador padrão
        private void iniciaAplicacao()
        {
            ApplicationDbContext db = new ApplicationDbContext();

            var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(db));
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(db));

            string UserPWD = "aBC_123";

            // criar a Role 'Médico'
            if (!roleManager.RoleExists("Medico"))
            {
                // não existe a 'role'
                // então, criar essa role
                var role = new IdentityRole();
                role.Name = "Medico";
                roleManager.Create(role);

                // criar um utilizador 'Médico'
                var User = new ApplicationUser();
                User.UserName = "fernando@gmail.com"; // login
                User.Email = "fernando@gmail.com";
                User.Nome = "Fernando Sousa";

                var chkUser = userManager.Create(User, UserPWD);
            }

            // Criar a role 'Terapeuta'
            if (!roleManager.RoleExists("Terapeuta"))
            {
                var role = new IdentityRole();
                role.Name = "Terapeuta";
                roleManager.Create(role);

                // criar um utilizador 'Terapeuta'
                var User = new ApplicationUser();
                User.UserName = "susana@gmail.com"; // login
                User.Email = "susana@gmail.com";
                User.Nome = "Susana Pereira";

                var chkUser = userManager.Create(User, UserPWD);

            }

            // Criar a role 'Administrativo'
            if (!roleManager.RoleExists("Administrativo"))
            {
                var role = new IdentityRole();
                role.Name = "Administrativo";
                roleManager.Create(role);

                // criar um utilizador 'Administrativo'
                var User = new ApplicationUser();
                User.UserName = "pedro.f@gmail.com"; // login
                User.Email = "pedro.f@gmail.com";
                User.Nome = "Pedro Ferreira";

                var chkUser = userManager.Create(User, UserPWD);

                //Adicionar o Utilizador à respetiva Role-Administrativo-
                if (chkUser.Succeeded)
                {
                    var result1 = userManager.AddToRole(User.Id, "Administrativo");
                }
            }

        }
    }
}
