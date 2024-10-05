Basic DI Container for Unity engine, now with an API similar to Zenject. Supports registrations as singletons, transients, and instance bindings. Tags (identifiers) are also supported. The container now uses interfaces for better flexibility and future integration with Zenject.

Installation
Just download and import the Unity package file from this page.

What It Can Do
Zenject-Like API
BaCon now supports a fluent API similar to Zenject, allowing you to use methods like Bind<T>(), WithId(), FromMethod(), AsTransient(), and AsSingle() for configuring your bindings.

Types of Registrations
As Single
Registers a singleton instance. Only one instance is created, and the same instance is returned on each resolve.

Registration:

csharp
Копіювати код
container.Bind<MyService>()
         .FromMethod(_ => new MyService())
         .AsSingle();  // Without tags

container.Bind<MyService>()
         .WithId("my_awesome_tag_1")
         .FromMethod(_ => new MyService())
         .AsSingle();  // Using tags

container.Bind<MyService>()
         .WithId("my_awesome_tag_2")
         .FromMethod(_ => new MyService())
         .AsSingle();  // Using tags

container.Bind<AnotherService>()
         .FromMethod(c => new AnotherService(c.Resolve<MyService>()))
         .AsSingle();  // Using resolved instance as a parameter
Resolving:

csharp
var serviceWithoutTag = container.Resolve<MyService>();   // Without tags
var serviceWithTag1 = container.Resolve<MyService>("my_awesome_tag_1");  // Using tags
var serviceWithTag2 = container.Resolve<MyService>("my_awesome_tag_2");  // Using tags
As Transient
Creates a new instance of the object each time it is resolved.

Registration:

csharp
container.Bind<ExampleProjectTransient>()
         .FromMethod(_ => new ExampleProjectTransient())
         .AsTransient();  // Without tags
Resolving:

csharp
var a = container.Resolve<ExampleProjectTransient>();
var b = container.Resolve<ExampleProjectTransient>();   // a != b
From Instance
Registers an already created instance. This binding is always registered as a singleton.

Registration:

csharp
var instance = new MyInstance();
container.Bind(instance);  // Without tags

var taggedInstance = new MyInstance();
container.Bind<MyInstance>()
         .WithId("my_awesome_instance_tag")
         .FromInstance(taggedInstance)
         .AsSingle();   // Using tags
Resolving:

csharp
var instance = container.Resolve<MyInstance>();
var instanceWithTag = container.Resolve<MyInstance>("my_awesome_instance_tag");
Quick Instance Binding
You can bind an instance without specifying the type explicitly:

csharp
container.Bind(feature);  // Automatically registered as a singleton
Note: When you use Bind(instance) without further configuration, the instance is automatically registered as a singleton, and you cannot add additional configurations like tags. If you need to configure the binding further, use the generic Bind<T>() method.

Scoping
BaCon supports container hierarchies. A child container can resolve dependencies registered in its parent container, but the parent container cannot resolve dependencies registered in its child containers.

Example:

csharp
// Game entry point
IDIContainer projectContainer = new DIContainer();   // Can resolve only projectContainer instances
projectContainer.Bind<MyProjectService>()
                .FromMethod(_ => new MyProjectService())
                .AsSingle();

// Scene 1 entry point
IDIContainer sceneContainer1 = new DIContainer(projectContainer);   // Can resolve projectContainer instances
sceneContainer1.Bind<MyFirstSceneService>()
               .FromMethod(_ => new MyFirstSceneService())
               .AsSingle();

// Scene 1 resolving
var projectService = sceneContainer1.Resolve<MyProjectService>();
var sceneService = sceneContainer1.Resolve<MyFirstSceneService>();
// var sceneServiceFromProject = projectContainer.Resolve<MyFirstSceneService>();   // Error: cannot resolve child bindings from parent container

// Scene 2 entry point
IDIContainer sceneContainer2 = new DIContainer(projectContainer);   // Can resolve projectContainer instances
sceneContainer2.Bind<MySecondSceneService>()
               .FromMethod(_ => new MySecondSceneService())
               .AsSingle();

// Scene 2 resolving
var projectService = sceneContainer2.Resolve<MyProjectService>();
var sceneService = sceneContainer2.Resolve<MySecondSceneService>();
// var sceneServiceFromProject = projectContainer.Resolve<MySecondSceneService>();   // Error: cannot resolve child bindings from parent container
Interfaces for Flexibility
BaCon uses interfaces for its container and bindings, allowing for easy swapping of implementations. This design prepares your codebase for a smooth transition to Zenject if you decide to adopt it in the future.

Interfaces:

IDIContainer: Represents the dependency injection container.
IBindingBuilder<T>: Allows configuring bindings through a fluent API.
Example Usage
Registering a Service:

csharp
IDIContainer container = new DIContainer();

container.Bind<IMyService>()
         .FromMethod(_ => new MyService())
         .AsSingle();  // Registers MyService as a singleton implementing IMyService
Resolving a Service:

csharp
var myService = container.Resolve<IMyService>();  // Resolves the singleton instance of MyService
Registering with a Tag:

csharp
container.Bind<IMyService>()
         .WithId("specialService")
         .FromMethod(_ => new MyService())
         .AsTransient();  // Registers a transient binding with a tag

var specialService = container.Resolve<IMyService>("specialService");  // Resolves a new instance each time
Binding an Existing Instance:

csharp
var existingInstance = new MyService();

container.Bind(existingInstance);  // Automatically registers as a singleton

// Alternatively, with additional configuration:
container.Bind<IMyService>()
         .WithId("existingService")
         .FromInstance(existingInstance)
         .AsSingle();
Scoping with Child Containers:

csharp
IDIContainer parentContainer = new DIContainer();

parentContainer.Bind<IParentService>()
               .FromMethod(_ => new ParentService())
               .AsSingle();

IDIContainer childContainer = new DIContainer(parentContainer);

childContainer.Bind<IChildService>()
              .FromMethod(_ => new ChildService())
              .AsSingle();

// Resolving from child container
var parentService = childContainer.Resolve<IParentService>();
var childService = childContainer.Resolve<IChildService>();

// Resolving from parent container
// var childServiceFromParent = parentContainer.Resolve<IChildService>();  // Error: cannot resolve child bindings from parent
Transitioning to Zenject
BaCon's API is designed to closely resemble Zenject's, making it easier to transition when you're ready. Since your code interacts with interfaces, swapping out BaCon for Zenject requires minimal changes.

Example with Zenject:

csharp
using Zenject;

DiContainer zenjectContainer = new DiContainer();
IDIContainer container = new ZenjectContainer(zenjectContainer);  // Assuming ZenjectContainer implements IDIContainer

container.Bind<IMyService>()
         .FromMethod(_ => new MyService())
         .AsSingle();

var myService = container.Resolve<IMyService>();
Disposing the Container
BaCon implements IDisposable for proper resource management. Ensure that you dispose of the container when it's no longer needed to release any resources held by disposable dependencies.

Example:

csharp
container.Dispose();
Note: Be cautious with cyclic dependencies involving the container itself to avoid StackOverflowException during disposal. Avoid registering the container within itself.

Best Practices
Avoid Registering the Container in Itself: Do not bind IDIContainer to the container instance within the container to prevent cyclic dependencies.
Use Interfaces for Dependencies: Register and resolve dependencies using interfaces rather than concrete classes to promote loose coupling.
Configure Bindings Completely: Ensure you call AsSingle() or AsTransient() after setting up your bindings to properly register them.
Handle Disposable Dependencies: If your dependencies implement IDisposable, they will be disposed of when the container is disposed.
Conclusion
BaCon provides a simple yet flexible DI container for Unity, with an API that mirrors Zenject's. It supports advanced features like scoping and tagged bindings, making it suitable for various project structures. By using interfaces, it ensures that your code remains flexible and maintainable, and prepares you for an easy transition to Zenject if desired.






