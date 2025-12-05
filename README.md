# A Unity Template for Clean Architecture
This is the repo to my master's thesis "Modular Game Architecture in Unity: A Case Study of Clean Architecture and Platform Optimization in Unbreachable".



## Abstract of my thesis
Publishing games across multiple platforms is becoming more attractive. However, it introduces significant architectural complexity due to requirements and different APIs. This thesis addresses the challenge of designing a modular, maintainable software architecture for multi-platform Unity games. Using the Design Science Research methodology, an architectural artifact is designed, implemented, and evaluated using a real-world use case, Unbreachable, an ongoing game project by Buckfish.

The designed architecture uses established design patterns combined with principles of Clean Architecture. This includes event-driven communication, dependency injection, and the Model-View-Presenter pattern. The use case was evaluated using code metrics, Windows and Steam Deck runtime benchmarks, and tests in the Unity Editor.

The results indicate that the software architecture improves maintainability and scalability, while still providing a desired performance. These findings demonstrate that architectural principles can be effectively applied to game development in Unity projects. The research contributes a validated software architecture, a Unity template, and empirical insights in developing scalable and maintainable games.

![The overview of the designed architecture. The primary classes, categories, and concepts are illustrated.](Figures/DesignOverview.png)

# Layers
Layering code into clear concerns helps improve code quality and maintainability. This separation reduces coupling and ensures that gameplay rules are independent from Unity-specific implementations. Unit testing can be done effectively outside of the Unity runtime. The layered architecture creates a more modular, flexible, and long-term sustainable codebase.

Four layers are introduced, which are most closely aligned with the proposed Clean Architecture layers. The most significant adaptations are the renaming and the change in the presentation layer's purpose. Each layer is explained in detail below.

The external layer has a special role. It has access to every layer and each platform realization. The most recognizable class here is the [bootstrapper](https://github.com/Lepi009/Clean-Architecture-Unity-Template/edit/master/README.md#bootstrapper). Since it implements platform-specific behaviour, it cannot be packaged into the presentation layer, as that layer is split across platforms.

## Domain Layer
The domain layer is equal to the entity layer in the Clean Architecture. It includes the most robust rules, objects, and behavior. This layer is entirely self-contained and has no dependencies on any layers above it. A change in this layer often requires a complete recompilation of all other scripts.

This layer has no references to other assemblies, and no engine classes or functions are available. Thus, commonly used structs and classes such as rects, vectors, and matrices are implemented here, since the Unity ones are not available. Further, there are the [ServiceLocator](Assets/Scripts/01-Domain/Basics/ServiceLocator.cs) and the [event bus](Assets/Scripts/01-Domain/Event%20System).

## Application Layer
The application layer is the second layer and is similar to the second layer in Clean Architecture. This layer only depends on the domain layer. It contains all use cases, executes the game logic, and defines abstractions.

Since the use cases are game-dependent, there are no classes that are always contained in the application layer in any game. Games with an in-game shop could implement a shop service to handle item purchases in this layer. Also, an inventory service that adds or removes items could be implemented here. The inventory itself would be implemented in the domain layer. Any quest service could also be implemented here to update quest progress based on events.

One further purpose of this layer is to define the interfaces implemented by layers above it. Through dependency injection, the concrete implementation is injected at runtime. Those interfaces should often define functions with an asynchronous callback. Because the concrete implementation could be asynchronous, this must be considered in those interfaces and in how they are used in this layer. For example, reading a file can be handled synchronously; however, retrieving a file from the cloud should be done asynchronously and could fail. The service's interface must handle the most complex case. Otherwise, the concrete implementation influences a service's behaviour, which violates the Liskov Substitution Principle.

Additionally, the implemented services fire specific events that the layers above can subscribe to. A heavy-event-driven service could be the audio system, because it needs to adapt the music to the current state of the game and respond to many different events to trigger sound effects accordingly. For instance, a change in the player's money balance could trigger a buying or selling sound.

## Infrastructure Layer
The infrastructure layer serves as the translation layer and is similar to the third layer of the Clean Architecture. This is the first layer that has access to engine-specific implementations. 

In the third layer, most of the interfaces, defined by the application layer, are implemented. The concrete implementations may vary between platforms. Thus, multiple implementations could be found for a single interface. More can be found in the [platform adapters](https://github.com/Lepi009/Clean-Architecture-Unity-Template/edit/master/README.md#platform-adapters) section.


Data transfer objects (DTOs) are often-used classes in the infrastructure layer. Those are used to receive data and translate it into the corresponding domain entities. This translation keeps the domain independent of the providing services. For instance, changing a name in a JSON file only affects the DTO, not the entity in the domain layer.

Presenters are another often-found class in the layer. Those are part of the [MVP pattern](https://github.com/Lepi009/Clean-Architecture-Unity-Template/edit/master/README.md#user-interface). These can also be seen as a translator between the core application and a concrete user interface.

## Presentation Layer
The outermost layer is the presentation layer. This layer has been altered and differs from the fourth layer defined by Clean Architecture. More code is found in this layer than in Clean Architecture.

All necessary MonoBehaviours are implemented in this layer. Those include the views that are attached to a UI prefab. Concrete [input layers](https://github.com/Lepi009/Clean-Architecture-Unity-Template/edit/master/README.md#input) and input schemes are also implemented in this layer. Those implementations often change in the progress of development due to new user expectations and experience. Thus, the presentation layer is typically the layer that undergoes the most frequent changes.

# Core Systems
## Event Bus
## Bootstrapper
## Service Locator
## Async Operations using Coroutines
## Platform adapters

# User Interface


# Input



# How to Build

