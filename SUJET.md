# Donde esta la biblioteca

## Sujet 

Vous allez réaliser une petite application qui vous permettra de gérer une liste de bibliothèque ayant des livres à louer.
Une petite BDD sera jointe au projet qu'on va essayer d'enrichir au maximum.
L'idée est d'être en autonomie par Groupe de 2.

Le but du projet est de voir un maximum des choses que .NET peut vous apporter donc n'hésitez pas à demander au prof.

Pensez aussi à regarder les raccourcis utiles en bas de page.

Vous pouvez *Fork* ce repo Git, le cloner en local et travailler directement dedans. 

## Les mots clefs

- *Solution*
  - La racine de votre application .NET qui permet d'avoir la vision globale des projet
  - Manifesté sous la forme d'un fichier *.sln*, c'est ce fichier qu'il faut ouvrir pour ouvrir votre espace de travail
  - Va contenir un ou plusieurs projet
- *Projet* :
  - Bloc de construction de l'application
  - Va générer une dll par projet
  - Facilement extractable d'une solution à une autre
- *Nuget* :
  - Beignet de poulet frit (existe aussi en version vegan)
  - Gestionnaire de package
  - Permet d'intégrer des dll à votre projet pour étendre les fonctionnalités de votre code (AutoMapper, ORM...)

### Critère de qualité

- Commits réguliers, au moins à chaque étape
- Indentation
- En Anglais
- Privilégier les interfaces aux classes concrètes (normalement en suivant bien le projet ça devrait être facile)
- Nommage :
  - "Singulier" pour un objet simple et "Pluriel" pour une liste 
  - **PascalCase** : Pour les noms de classe et interface...
  - **camelCase** : Pour les variables
  - **_camelCase** : Pour les variables déclaré au niveau de la classe
  - Nom d'interface commençant par un **I**
  - Nom de classe abstraite par un **A**

## TODO

### Etape 1 : Créer sa solution .NET

Créez un nouveau projet via Visual Studio 2022, puis sélectionner une *application console* nommée `LibraryManager`.

Si vous n'avez pas d'explorateur de solution sur votre IDE, vous pouvez aussi l'ajouter via le menu `Affichage > Explorateur de Solution`.

Pensez à ajouter via le *gestionnaire de package Nuget* sur votre projet : Microsoft.Extensions.Hosting

Et mettre en place son architecture de projets en ajoutant via VS des *Librairies de classes* :
- `BusinessLayer` : Couche métier; on va y mettre toute la logique métier
- `Services` : Couche services intermédiaire; va permettre d'orchestrer les besoins et de relier d'autres couches entre elles
- `BusinessObjects` : Couche contenant vos objets métier (objets de base de données ou de travail)
- `DataAccessLayer` : Couche permettant l'accès aux données; on y retrouvera notamment les repository

PS: Votre projet créer avec la solution fait office de couche d'entrée à l'application et configuration

Créer une méthode Main dans le `Program.cs` grâce aux recommandations VS `Alt + Entrée` à la l'intérieur du fichier, puis ajouter dans la classe Program la méthode :

```cs
    private static IHost CreateHostBuilder(IConfigurationBuilder configuration)
    {
        return Host.CreateDefaultBuilder()
            .ConfigureServices(services =>
            {
              // Configuration des services
            })
            .Build();
    }
```

⚠️ Pensez à commit.

### Etape 2 : Préparer son architecture

Maintenant passons à l'implémentation de notre architecture.

1. Dans votre projet `BusinessObjects`, créez un dossier `Entity`, puis dans ce dossier créez les objets correspondants aux tables du fichier `LibraryInit.sql`

Pour le type des types de livres, pensez à créer un `enum`. Pas besoin de créer un fichier pour la table de `Stock`.

Dans le cadre d'une relation `OneToMany` (1..\*) ou `ManyToMany` (\*), le **Many** se manifeste sous la forme d'une liste (Ex : `IEnumerable<ClassA>`) et le **One** sous la forme d'un objet simple (Ex : `ClassA`).

2. Dans votre projet `DataAccessLayer`, créez un dossier `Repository`, puis dans ce dossier une classe `BookRepository`

Vous y créerez les méthodes `GetAll()` qui retournera un `IEnumerable<Book>` et `Get(int id)` qui retounera un `Book`, vous pouvez `return` des objets vides à ce stade.

PI : Vous aurez besoin d'ajouter des références d'un projet à un autre pour permettre d'utiliser vos entités à l'extérieur de leur projet respectif.

Répétez le même schéma pour chacune de vos entités.

3. Dans votre projet `BusinessLayer`, créez un dossier `Catalog`, puis dans ce dossier une classe `CatalogManager` qui contiendra les méthodes `DisplayCatalog()`, `DisplayCatalog(Type type)` et `FindBook(int id)` qui utiliseront les Repository

4. Dans votre projet `Services`, créez un dossier `Services`, puis dans ce dossier une classe `CatalogService` qui contiendra les méthodes `ShowCatalog()`, `ShowCatalog(Type type)` et `FindBook(int id)` qui utiliseront le `CatalogManger`

⚠️ Pensez à commit.

### Etape 3 : LINQ

LINQ (prononcé line-cue) est langage d'interrogation pour vos tableaux en .NET. C'est un nuget à installer.

Celui vos permettra de filtrer vos tableaux et d'ajouter une granularité supplémentaire.
Il est possible d'utiliser LINQ via le langage correspondant (comme du SQL) ou d'utiliser des méthodes d'extensions sur vos listes (Ex : `_voitures.Where(x => x.Price < 2000)`)

Dans ce contexte, on utilisera le langage LINQ comme dans la documentation Microsoft mais sachez que les deux sont possibles.

1. Dans les `BusinessLayer` et `Services`, ajoutez une méthode pour ne remonter que les livres de type "Fantasy"
2. Dans les `BusinessLayer` et `Services`, ajoutez une méthode pour remonter le livre le mieux noté

Pour plus d'informations : [LINQ - Microsoft](https://learn.microsoft.com/fr-fr/dotnet/csharp/linq/)

⚠️ Pensez à commit.

### Etape 4 : Injection de dépendance

Il s'agit ici d'un concept extrêmement important lors du développement d'une application aujourd'hui.
On ne développe non plus à partir de classe concrète mais à partir des interfaces afin de réduire le couplage de vos applications à l'implémentation.

L’injection de dépendances consiste, pour une classe, à déléguer la création de ses dépendances au code appelant qui va ensuite les injecter dans la classe correspondante. De ce fait, la création d’une instance de la dépendance est effectuée à l’extérieur de la classe dépendante et injectée dans la classe.

![Dependency Injection](/schemas/dependancy_injection.drawio.png)

Par exemple, nous possède une classe concrète `ApiACaller` que j'instancie à plusieurs endroits dans mon code. 
Demain, on nous demande de la remplacer par un `ApiBCaller` car la source de doit changer.
Plutôt que d'avoir à changer toutes références à notre classe concrète, nous allons mettre dans le constructeur notre interface qui vous donnera les fonctions disponibles.
Et à plus haut niveau, nous lui injecterons la classe concrète qui correspondera à cette interface.

A la compilation, la classe concrète correspondante est inconnu. Par contre, à l'exécution, cette classe est injecté à chaque qu'une référence à celle-ci est faite via l'interface.

De ce fait, lorsque que nous changerons l'implementation via une nouvelle classe, nous aurons juste besoin de changer la configuration correspondante.

```cs
  public interface IApiCaller {
    object Call();
  }

  public class ApiACaller : IApiCaller {
    public object Call() {
      return ResultA;
    }
  }

  public class ApiBCaller : IApiCaller {
    public object Call() {
      return ResultB;
    }
  }

  // Before
  public Constructor() {
    _caller = new ApiACaller();
  }

  // After
  public Constructor(IApiCaller apiCaller) {
    _caller = apiCaller;
  }
```

En allant plus loin, on peut récupérer toutes vos classes de cette manière :

```cs
  public ClassA(InterfaceB b){
    _b = b;
  }

  public ClassB(InterfaceC c){
    _c = c;
  }
 
  public ClassD(InterfaceA a, InterfaceB b) {
    _a = a;
    _b = b;
  }

  // Et ainsi de suite
```

On peut injecter un Singleton ! Renseignez vous sur la documentation pour connaître les cycles de vie des objets injecté. 

Pour réaliser de l'injection de dépendance, extrayez une interface de vos classes concrètes ayant de la logique et instanciés ailleurs dans votre code (Ex : Services...)

Pour vos repository, on fera un peu différemment. Vous allez créer une seule interface `IGenericRepository` qui prendra en paramètre un type générique. Aidez-vous de la documentation.

Pour récupérer votre classe correspondante dans le `Main` et tester en reprenant l'exemple du `IApiCaller` :

```cs
  IApiCaller apiCaller = host.Services.GetRequiredService<IApiCaller>();
  apiCaller.Call();
```

Pour plus d'informations : 
- [Injection de dépendance - Microsoft](https://learn.microsoft.com/fr-fr/aspnet/core/fundamentals/dependency-injection?view=aspnetcore-8.0)
- [Classes et méthodes générique - Microsoft](https://learn.microsoft.com/fr-fr/dotnet/csharp/fundamentals/types/generics)

⚠️ Pensez à commit.

### Etape 5 : EntityFramework

Avec l'aide de la base de données SQLite fournit en annexe, vous allez implémenter l'ORM **EntityFramework**.

Le but ici est d'au lieu de renvoyer des listes vides au niveau de vos `Repositories`, de renvoyer les infos qui sont stockées en base de données.

Dans votre `DataAccessLayer`, créez un dossier `Contexts` et un fichier `LibraryContext` qui implémentera la classe `DbContext`, servez-vous de la documentation pour remplir votre DbContext.  

Pensez à l'injecter, pour une fois on utilisera une classe concrète. Vous aurez sûrement un autre Nuget à récupérer sur vos deux projets.

```cs
  services.AddDbContext<LibraryContext>(options =>
    options.UseSqlite("Data Source={path};"));
```

Dans vos respositories, utilisez le `LibraryContext` injecté pour récupérer le contenu de la base.

Pour plus d'informations : [EntityFramework - Microsoft](https://learn.microsoft.com/fr-fr/ef/core/)

Pour consulter votre base de données, je vous conseille l'utilisation de [DBeaver](https://learn.microsoft.com/fr-fr/dotnet/csharp/fundamentals/types/generics)

⚠️ Pensez à commit.

### Etape 6 : TU

Créez un dossier `Tests` et puis un nouveau projet `Services.Test`.
Créez une classe `CatalogServiceTest`.

Implémentez un test unitaire sur chaque méthode de votre `CatalogService` en pensant à Mock le retour de votre `CatalogManager`pour bien tester unitairement votre méthode.

Pour plus d'informations : [TU avec C# - Microsoft](https://learn.microsoft.com/fr-fr/dotnet/core/testing/unit-testing-with-mstest)

⚠️ Pensez à commit.

### Etape 7 : API

On va maintenant mettre en place une API. Pour rappel, une API est une interface logiciel sur laquel vous pourrez vous connecter et récupérer des informations via requête HTTP. Elle vous renverra un résultat sous la forme d'un JSON.

Pour cela, vous allez créer un nouveau projet de type `ASP.NET Core WebAPI`, sans authentification que vous allez appeler `LibraryManager.Hosting`.

Une fois créée, vous allez mettre ce projet en tant que projet de démarrage. 

Votre nouveau `Program.cs`, va ressembler à ça :

```cs
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers(); 
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

/*
Les Middleware ajoutés avant le builder seront récupérer par l'application
*/

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
```

Transformez votre fichier grâce aux recommandations VS `Alt + Entrée`.

Ajoutez à votre builder les services de votre précédent `Program.cs`.

Observez la classe créé :

```cs
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
        }

        [HttpGet(Name = "GetWeatherForecast")]
        public IEnumerable<WeatherForecast> Get()
        {
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = Summaries[Random.Shared.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
```
Les éléments entre crochets sont appelés des attributs. Ils définissent des métadonnées qui sont lisible durant l'exécution.

Vous pouvez créé vos propres attributs, mais les attributs par défaut définissent des comportements :
- `[ApiController]` définit les comportements par défaut d'une API REST
- `[Route("[controller]")]` définit la route pour accéder à un controller; le `[controller]` désigne le nom de la classe en tant que endpoint 
- `[HttpGet(Name = "GetWeatherForecast")]` définit la méthode la méthode attendu

Donc pour accéder à cette API, nous utiliserons `GET localhost:53000/WeatherForecast/GetWeatherForecast`. Il existe même des attributs pour l'authentification.

Après l'explication, place à la pratique.

Créez un fichier `BookController` qui va commprendre les méthodes *GET* suivants :
- book
- book/{id}
- book/{type}
- book/topRatedBook

Pour tester votre API, installez [Postman](https://www.postman.com/).

Pour plus d'informations : [Tutoriel ASP.NET Core Web API- Microsoft](https://learn.microsoft.com/fr-fr/aspnet/core/tutorials/first-web-api?view=aspnetcore-8.0&tabs=visual-studio)

⚠️ Pensez à commit.

### Etape 8 : Vous allez plus vite que prévu ?

Améliorer votre bibliothèque :
- Créez de nouveaux endpoint pour accéder aux différentes tables.
- Ajoutez une table `Client` pour permettre à un client d'emprunter un livre et d'être black listed.
- Ajoutez une notion de quantité à votre stock.
- Permettre à un client d'emprunter des livres, de les rendres.


### Raccourcis utiles 

- Recommandation VS : `Alt + Entrée` => Hyper utile, n'hésitez pas à en abuser
- Renommer un élément et ses références : `(Hold) CTRL + R + R`
- Créer une propriété : `(Write) prop + Tab`
- Créer un constructeur: `(Write) ctor + Tab`
- Redirection sur la classe concernée : `F12`
- Redirection sur la classe concrète concernée : `Ctrl + F12`
- Afficher le contenu de l'élément dans un encart : `Alt + F12`
- Lancer en Debug : `F5`
- Faire continuer le programme : `F5`
- Instruction suivante : `F10`
- Instruction suivante dans la méthode : `F11`
- Ajouter une référence à un projet : `Clic droit sur un Projet > Ajouter > Ajouter une référence à un projet`

### Contacts

- Mail : erwann.fiolet@gmail.com
- Discord : byabyakar
