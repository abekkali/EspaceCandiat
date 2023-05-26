using Microsoft.AspNetCore.Identity;

namespace EspaceCandiat.Data
{
    public class RoleInitializer : IHostedService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public RoleInitializer(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        public async Task StartAsync(CancellationToken cancellationToken)
        {
            using var scope = _scopeFactory.CreateScope();
            var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

            if (!await roleManager.RoleExistsAsync("Admin"))
            {
                var role = new IdentityRole("Admin");
                await roleManager.CreateAsync(role);
            }
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            return Task.CompletedTask;
        }
    }

}
