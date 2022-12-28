<h1 align="center">CRON</h1>

<p align="center">Uma API que faz scraping diário de informações nutricionais de produtos na <a href="https://world.openfoodfacts.org/">Open Food Facts</a> de maneira simples e rápida.</p>

### 🛠 Tecnologias

- [ASP.NET](https://dotnet.microsoft.com/en-us/apps/aspnet)
- [Entity Framework](https://learn.microsoft.com/en-us/ef/)
- [MySQL](https://www.mysql.com/)
- [Quartz.NET](https://www.quartz-scheduler.net/)

### 📝 Requisitos

É necessário ter o  [NET 6](https://dotnet.microsoft.com/en-us/download/dotnet/6.0) e o [MySQL](https://dev.mysql.com/downloads/installer/). 


### 🧐 Como Usar

Antes de configurar e rodar a aplicação precisamos gerar uma API Key da SendGrid para o envio de emails, saiba como gerar a [aqui](https://www.twilio.com/blog/send-emails-using-the-sendgrid-api-with-dotnetnet-6-and-csharp). Depois da criação da API Key, podemos configurar ela de forma segura:

```bash
# Ainda em /src/CRON.Api execute
$ dotnet user-secrets init

# seta o valor da key para "SendGridApiKey"
$ dotnet user-secrets set SendGridApiKey [Key que você gerou aqui]
```

Com o sucesso de setar a a key da API, podemos prosseguir.

##### Rodando os Testes de Endpoints
Na raíz do diretório basta executar os seguintes comandos:

```bash
# Para listar todos os testes 
$ dotnet test -t 

# Executa os testes
$ dotnet test
```

##### Configurando o EF com Mysql

Acessando [src/CRON.Api](src/CRON.Api) mude em [src/CRON.Api/appsettings.Development.json](src/CRON.Api/appsettings.Development.json), os valores de User ID e Password da Connection String, de acordo com que o que você definiu durante a instalação do MySQL. 

<p>
Para criação da tabela com EF, é necessário ter a ferramenta para o gerenciamento, execute o comando para a instalar:
</p>

```bash
$ dotnet tool install --global dotnet-ef --version 6.0.12
```
Após as mudanças, podemos executar os seguintes comandos para criar a tabela no banco de dados:  
```bash
# Acesse a pasta do projeto da API no terminal/cmd
$ cd /src/CRON.Api

# Criando a Tabela
$ dotnet ef database update -c DataContext -p ../CRON.Data/CRON.Data.csproj
```

#### Rodando a Aplicação
Após a configuração do EF com o banco de dados, podemos rodar a aplicação:

Com o sucesso da criação da Key, podemos executar a aplicação:

```bash
# Ainda em /src/CRON.Api
$ dotnet run
```

E assim poderemos acessar a documentação da API no localhost através da url: https://localhost:7184/swagger/

#### Sobre a automatização do Scraping

Em [/src/CRON.Api/Extensions/QuartzSheduleSetup.cs](/src/CRON.Api/Extensions/QuartzSheduleSetup.cs), foi definido dois ScheduleJob, o que está comentado é o que executa diariamente de acordo com a [Cron Expression](http://www.quartz-scheduler.org/documentation/quartz-2.3.0/tutorials/crontrigger.html) definida em [src/CRON.Api/appsettings.json](src/CRON.Api/appsettings.json), e a outra que não está comentada e que é executada assim que a aplicação for inicializada (Fins Demonstrativos).
Para verificar o Job que é executado diariamente de acordo com o horário definido, basta alterar a [Cron Expression](http://www.quartz-scheduler.org/documentation/quartz-2.3.0/tutorials/crontrigger.html) definida em appsettings.json de acordo com sua preferência e comentar/apagar o SheduleJob que estava sendo executado, assim dentro do método UseProductScrapingSheduler irá ficar da seguinte maneira:
```cs
...
// Scraping 
services.AddQuartz(q =>
{
    // Provide DI to jobs
    q.UseMicrosoftDependencyInjectionJobFactory();

    // Take a cron expression from appsettings.json and execute ProductJob once a day
    q.ScheduleJob<ProductJob>(trigger => trigger
        .WithIdentity("Product Scraping.")
        .WithCronSchedule(time)
    ); 
});

// For respect lifecycle of Application
services.AddQuartzServer(options =>
{
    options.WaitForJobsToComplete = true;
});
```

#### Sobre o sistema de alertas 
Para o sistema de alertas, em caso de alguma falha na sincronização dos produtos com o banco de dados, foi optado pelo envio de emails, podendo ser configurado em [src/CRON.Api/appsettings.Development.json](src/CRON.Api/appsettings.Development.json) o destinatário.


#### This is a challenge by Coodesh
