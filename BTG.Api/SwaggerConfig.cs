namespace BTG.Api
{
    public static class SwaggerConfig
    {
        public static void UseSwaggerConfig(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API v1");
                c.RoutePrefix = string.Empty;  // Define a página principal do Swagger UI no root
            });
        }

        public static void AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo
                {
                    Title = "Desafio - BTG",
                    Version = "v1",
                    Description = "API do Desafio BTG",
                    Contact = new Microsoft.OpenApi.Models.OpenApiContact
                    {
                        Name = "Maximiliano Pires",
                        Email = "maxpiresalves@gmail.com"
                    }
                });
            });
        }
    }
}
