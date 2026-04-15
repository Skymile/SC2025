const string raw = @"\documentclass{article}
\usepackage[a4paper, total={7in, 11in}]{geometry}
\usepackage[utf8]{inputenc}
\usepackage{polski}
\usepackage{xcolor}
\usepackage{hyperref}

\usepackage{microtype     } % Better text justify
\usepackage{wrapfig       } % Picture wraps
\usepackage{graphicx      } % Including graphics
\usepackage{hyperref      } % Hyperlinks
\usepackage{xcolor        } % Custom colors
\usepackage{tikz          } % Drawing rectangles
\usepackage{fontawesome   } % Icons
\usepackage{polski}
\usepackage{amssymb} % For square icon

% Colors 3C4448
\definecolor{bg}              {HTML}{222424} % Page Background
\definecolor{body}            {HTML}{FFFFFF} % Tasks Foreground Color
\definecolor{darkgray}        {HTML}{555555} % 
\definecolor{PageHeader}      {HTML}{35A9CC} % Page Header
\definecolor{SectionColor}    {HTML}{267790} % Section Bar Color
\definecolor{SubsectionColor} {HTML}{101010} % Subsection Bar Color
\definecolor{DescriptionColor}{HTML}{475657} % Description Color
% --------

\hypersetup{
    colorlinks,
    citecolor=black,
    filecolor=black,
    linkcolor=body,
    urlcolor=black
}


\newcommand{\cSection}[1]{
{\color{white}{\small
\section{#1}
}}}

\newcommand{\cSubsection}[1]{
{\color{white}{\small
\subsection{#1}
}}}

\newcommand{\citemize}[1]{
{\color{body}{\small
\begin{itemize}#1\end{itemize}
}}}

\newcommand{\work}[2]{
\noindent \makebox[\textwidth][l]{%
\hspace{-30pt}
  \colorbox{SubsectionColor}{\parbox{\dimexpr\paperwidth-13\fboxsep}{
  \vspace{0.1cm} 
  \addcontentsline{toc}{subsection}{#1}
  \textbf{\hyperlink{toc}{ #1 }}
  
  #2 
  \qquad \vspace{0.1cm}
  }}}
  \vspace{0.2cm}
  }

\newcommand{\header}[1]{
\addcontentsline{toc}{section}{#1}
\noindent\makebox[\textwidth][l]{%
  \hspace{-\dimexpr\oddsidemargin+1in}%
  \colorbox{SectionColor}{\parbox{\dimexpr\paperwidth-12\fboxsep}{
  \vspace{0.1cm} \large \ #1 \vspace{0.1cm}
  }}}
  \vspace{0.2cm}
  }

\begin{document}
\pagecolor{bg}
\color{white}

\tableofcontents
\addtocontents{toc}{\protect\hypertarget{toc}{}}

\pagebreak

\header{SOLID}
\work{SRP -- Single Responsibility Principle}
{A class should have only one reason to change, meaning it should have only one responsibility.}
\work{OCP -- Open/Closed Principle}
{Software entities (classes, modules, functions, etc.) should be open for extension but closed for modification.}
\work{LSP -- Liskov Substitution Principle}
{Objects of a subclass should be replaceable for objects of the superclass without altering the correctness of the program.}
\work{ISP -- Interface Segregation Principle}
{Many client-specific interfaces are better than one general-purpose interface.}
\work{DIP -- Dependency Inversion Principle}
{High-level modules should not depend on low-level modules. Both should depend on abstractions. Abstractions should not depend on details; details should depend on abstractions.}
\work{IoC -- Inversion of Control (Design Principle)}
{Separates ""what to do"" from ""when to do it."" The calling code should know as little as possible about the executing component, and vice versa.}
\work{DI -- Dependency Injection}
{A plugin-based architecture. Instead of creating dependencies internally, objects are provided with fully constructed instances (e.g., via constructor parameters), offering an alternative to instantiating dependencies inside the class itself.}

\header{GRASP}
\work{General Responsibility Assignment Software Patterns}
{A set of nine fundamental principles for object design and responsibility assignment, introduced by Craig Larman in his 1997 book *Applying UML and Patterns*. The GRASP patterns include: Controller, Creator, Indirection, Information Expert, Low Coupling, High Cohesion, Polymorphism, Protected Variations, and Pure Fabrication. These principles solve common software design problems and help document and standardize time-proven object-oriented techniques.}

\work{Controller}
{Assigns the responsibility of handling system events to a controller class that represents the overall system or a use-case scenario.}
\work{Creator}
{Assigns the responsibility of creating an instance of class A to class B if one or more of the following is true: B aggregates A, contains A, records instances of A, or closely uses A.}
\work{Indirection}
{Supports low coupling by introducing an intermediary object to mediate between other components or services.}
\work{Information Expert}
{Assigns a responsibility to the class that has the necessary information to fulfill it.}
\work{Low Coupling}
{Strives to reduce dependencies between classes to improve flexibility and maintainability.}
\work{High Cohesion}
{Keeps objects focused and manageable by ensuring each class or module has a well-defined and narrow responsibility.}
\work{Polymorphism}
{Assigns responsibility for behavior that varies by type to the types for which the behavior varies, using polymorphic operations.}
\work{Protected Variations}
{Protects elements from variations in other elements by encapsulating the unstable aspects behind stable interfaces.}
\work{Pure Fabrication}
{Introduces a class that doesn’t represent a concept in the problem domain, solely to achieve design goals such as high cohesion and low coupling.}

\header{Concepts}
\work{Lazy Loading}
{A design pattern that defers initialization of an object until it is needed, improving performance and resource usage.}
\work{Git Flow}
{A branching model for Git that defines strict roles for branches and organizes work around feature development, releases, and hotfixes.}
\work{CI/CD}
{Continuous Integration (CI) and Continuous Delivery/Deployment (CD) are DevOps practices that enable frequent, reliable software releases.}
\work{NuGet Package Consolidation}
{The process of aligning all dependencies across projects in a solution to use the same package versions, minimizing conflicts and simplifying maintenance.}

\header{Software Design Patterns -- Creational Patterns}
\work{Abstract Factory}
{Provides an interface for creating families of related or dependent objects without specifying their concrete classes.}
\work{Builder}
{Separates the construction of a complex object from its representation, allowing the same construction process to create different representations.}
\work{Dependency Injection}
{A class receives its dependencies from an external source (injector), rather than creating them internally.}
\work{Factory Method}
{Defines an interface for creating an object but lets subclasses decide which class to instantiate.}
\work{Lazy Initialization}
{Delays the creation of an object or calculation of a value until it is first needed. Related to the ""virtual proxy"" strategy in the Proxy pattern.}
\work{Multiton}
{Ensures a class has only a fixed set of named instances and provides global access to them.}
\work{Object Pool}
{Reuses objects from a pool rather than creating and destroying them repeatedly, to reduce resource allocation costs.}
\work{Prototype}
{Creates new objects by cloning a prototype instance, reducing the cost of creating objects from scratch.}
\work{Resource Acquisition Is Initialization (RAII)}
{Binds the lifecycle of a resource (like a file or lock) to the lifetime of an object, ensuring proper release of resources.}
\work{Singleton}
{Ensures a class has only one instance and provides a global access point to it.}

\header{Software Design Patterns -- Structural Patterns}
\work{Adapter, Wrapper, or Translator}
{Converts the interface of a class into another interface clients expect. Allows otherwise incompatible classes to work together.}
\work{Bridge}
{Separates an abstraction from its implementation so that the two can evolve independently.}
\work{Composite}
{Composes objects into tree structures to represent part-whole hierarchies, enabling clients to treat individual objects and compositions uniformly.}
\work{Decorator}
{Adds responsibilities to an object dynamically without altering its structure, offering a flexible alternative to subclassing.}
\work{Extension}
{Adds new functionality to a class hierarchy without modifying the existing structure.}
\work{Facade}
{Provides a simplified interface to a larger body of code, such as a complex subsystem.}
\work{Flyweight}
{Minimizes memory usage by sharing as much data as possible with similar objects.}
\work{Front Controller}
{Centralizes request handling for web applications, providing a single entry point for handling all requests.}
\work{Marker}
{Uses an empty interface to convey metadata or behavioral hints to classes.}
\work{Module}
{Groups related classes, methods, and singletons into a single logical unit for organization and reuse.}
\work{Proxy}
{Provides a surrogate or placeholder for another object to control access, reduce cost, or add functionality.}
\work{Twin}
{Simulates multiple inheritance by pairing two classes that work together to represent one conceptual entity in languages that do not support multiple inheritance.}

\header{Software Design Patterns -- Behavioral Patterns}
\work{Blackboard}
{An AI pattern that integrates diverse and independent components to collaboratively solve complex problems.}
\work{Chain of Responsibility}
{Avoids coupling the sender of a request to its receiver by allowing multiple objects a chance to handle the request.}
\work{Command}
{Encapsulates a request as an object, allowing parameterization of clients, queuing, logging, and support for undo operations.}
\work{Interpreter}
{Defines a grammar and an interpreter for interpreting sentences in a specific language.}
\work{Iterator}
{Provides a way to access elements of a collection sequentially without exposing the underlying structure.}
\work{Mediator}
{Encapsulates how a set of objects interact, promoting loose coupling and independent variation in communication logic.}
\work{Memento}
{Captures and externalizes an object’s internal state so it can be restored later, without violating encapsulation.}
\work{Null Object}
{Provides a non-functional object in place of a null reference to avoid null checks.}
\work{Observer or Publish/Subscribe}
{Establishes a one-to-many dependency so that when one object changes state, all dependents are automatically notified.}
\work{Servant}
{Provides common functionality to a group of classes. Typically implemented as a helper or utility class with static methods.}
\work{Specification}
{Encapsulates business rules and logic that can be combined using Boolean logic (AND, OR, NOT).}
\work{State}
{Allows an object to change its behavior when its internal state changes, appearing to change its class.}
\work{Strategy}
{Encapsulates a family of algorithms and makes them interchangeable to allow algorithm selection at runtime.}
\work{Template Method}
{Defines the structure of an algorithm, deferring specific steps to subclasses. Promotes code reuse and inversion of control.}
\work{Visitor}
{Encapsulates an operation to be performed on elements of an object structure, allowing new operations without changing the classes of the elements.}


\header{Concurrency patterns}

\work{Active Object}
{Decouples method execution from method invocation using its own thread of control. The goal is to introduce concurrency using asynchronous method calls and a scheduler to handle requests.}

\work{Balking}
{Only performs an action on an object if the object is in an appropriate state; otherwise, the action is simply ignored.}

\work{Binding properties}
{Synchronizes multiple object properties so that changes in one are automatically reflected in others. This is common in UI frameworks like WPF or JavaFX.}

\work{Compute kernel}
{Executes the same computation in parallel across many data points, typically using GPU-based processing for tasks such as matrix multiplication or convolution in neural networks.}

\work{Double-checked locking}
{Reduces the overhead of acquiring a lock by first checking the locking condition without synchronization. Only if necessary does it proceed with a synchronized block. Caution: must be implemented carefully to be thread-safe.}

\work{Event-based asynchronous}
{A concurrency pattern where operations are initiated asynchronously and completion is signaled through events or callbacks, minimizing thread contention.}

\work{Guarded suspension}
{Delays execution of a method until a precondition is true. Used in conjunction with synchronization mechanisms to wait for a resource or condition.}

\work{Join}
{A high-level pattern for coordinating message-passing between concurrent components. Often used in functional and distributed systems for synchronization.}

\work{Lock}
{A concurrency control mechanism that restricts access to shared resources by allowing only one thread at a time to access the resource.}

\work{Messaging design pattern (MDP)}
{Enables communication between components through message passing, promoting loose coupling and easier scalability.}

\work{Monitor object}
{An object that encapsulates synchronized methods, ensuring that only one thread can execute them at a time, enforcing mutual exclusion.}

\work{Reactor}
{Demultiplexes and dispatches events to appropriate handlers in a synchronous but non-blocking fashion. Often used in event-driven servers.}

\work{Read-write lock}
{Allows concurrent read access for multiple threads, but exclusive write access for a single thread. Improves performance in read-heavy applications.}

\work{Scheduler}
{Controls the order and timing in which threads or tasks are executed, often used to manage time-slicing and priorities in concurrency.}

\work{Thread pool}
{A collection of worker threads that are reused to execute multiple tasks. Reduces the overhead of thread creation and improves scalability.}

\work{Thread-specific storage}
{Provides each thread with its own isolated storage for global variables, preventing interference between threads.}

\work{Safe Concurrency with Exclusive Ownership}
{Design pattern where objects are owned exclusively, avoiding the need for synchronization. Proven either statically (as in Rust) or by disciplined code structure.}

\work{CPU atomic operation}
{Hardware-level instructions that ensure atomicity of basic operations like incrementing or setting a value. In .NET, these are provided via the \texttt{Interlocked} class.}

\header{.NET Specifics}

\work{sealed}
{Prevents a class from being inherited. Used to restrict extensibility and improve performance by allowing certain runtime optimizations.}

\work{interface, abstract class}
{An \texttt{interface} defines a contract that implementing classes must follow. An \texttt{abstract class} provides base functionality and can define both abstract and concrete members. Use abstract classes for shared code, and interfaces for capability definition.}

\work{Generic constraints}
{Used in generic programming to restrict the types that can be used as arguments for type parameters. For example: \texttt{where T : class}, \texttt{new()}, \texttt{IDisposable}, etc.}

\work{Collections.Generic}
{Namespace providing strongly-typed, efficient, and flexible collection classes such as \texttt{List<T>}, \texttt{Dictionary<TKey, TValue>}, \texttt{Queue<T>}, etc.}

\work{Collections.Immutable}
{Immutable collection types (like \texttt{ImmutableList<T>}) which provide thread-safe, non-modifiable data structures, ideal for functional-style programming or concurrent scenarios.}

\work{Collections.Concurrent}
{Thread-safe collection types like \texttt{ConcurrentDictionary<TKey, TValue>}, \texttt{BlockingCollection<T>}, and \texttt{ConcurrentQueue<T>} that enable high-performance multithreaded scenarios.}

\work{ArrayList}
{A non-generic collection from the \texttt{System.Collections} namespace that stores elements as \texttt{object}. It is less efficient and type-safe compared to generic collections.}

\work{dynamic, ExpandoObject, DynamicObject}
{\texttt{dynamic} allows bypassing compile-time type checking. \texttt{ExpandoObject} enables dynamic addition of members at runtime. \texttt{DynamicObject} allows creating custom dynamic behaviors through method overriding.}

\work{Lowering}
{Compiler optimization technique where high-level constructs are translated into lower-level operations. In .NET, this helps simplify the runtime's job by reducing constructs to a smaller set of primitives.}

\header{WPF Specifics}

\work{DynamicResource, StaticResource}
{\texttt{StaticResource} is resolved at load time and does not change if the resource changes. \texttt{DynamicResource} is resolved at runtime and updates automatically if the resource changes.}

\work{MVVM}
{Model-View-ViewModel: a design pattern that separates presentation logic (\texttt{ViewModel}) from UI (\texttt{View}) and business/data logic (\texttt{Model}). Facilitates testability and maintainability in WPF applications.}

\work{Bubbling, Tunneling, Direct Events}
{Event routing strategies in WPF: \textbf{Tunneling} starts at the root and travels down (\texttt{PreviewMouseDown}), \textbf{Bubbling} starts at the source and travels up (\texttt{MouseDown}), \textbf{Direct} events are raised and handled at the same element.}

\work{Drag \& Drop}
{WPF supports drag-and-drop operations with events like \texttt{DragEnter}, \texttt{DragOver}, \texttt{Drop}. Useful for intuitive UI data transfer or reordering elements.}

\header{SQL Specifics}

\work{Index, Indexes' Types}
{Indexes optimize query performance. Types include: Hash, Clustered, Non-Clustered, Unique, Columnstore, Filtered, Spatial, XML, Full-text, and indexes on included/computed columns.}

\work{Hash}
{Uses an in-memory hash table to map keys to locations. Ideal for point lookups, but inefficient for range queries. Often used in memory-optimized tables.}

\work{memory-optimized Non-clustered}
{Indexes for memory-optimized tables; data is not persisted to disk. Offers high performance and is ideal for scenarios requiring low latency.}

\work{Clustered}
{Defines the physical order of data in a table. Only one clustered index allowed per table. Ideal for range queries and primary keys.}

\work{Nonclustered}
{Stores index separately from table data. Points to the data row via row locator. Multiple nonclustered indexes can exist per table.}

\work{Unique}
{Enforces uniqueness of index key values. Can be defined on clustered or nonclustered indexes. Useful for enforcing data integrity.}

\work{Columnstore}
{Column-oriented index structure that improves performance for analytical queries on large datasets. Enables high compression and vectorized execution.}

\work{Index with included columns}
{Extends a nonclustered index by adding non-key columns, improving query performance by covering more queries without needing table access.}

\work{Index on computed columns}
{An index built on a column derived from an expression or function. Improves performance on queries that filter or sort using computed expressions.}

\work{Filtered}
{Indexes a specific subset of rows using a WHERE clause. Reduces index size and maintenance cost. Best for sparse columns or queries on predictable subsets.}

\work{Spatial}
{Indexes used to improve performance on spatial queries (e.g., geolocation). Applicable to geometry and geography data types.}

\work{XML}
{Indexes XML data stored in the \texttt{xml} type. Supports efficient querying and navigation of XML documents. Includes primary and secondary XML indexes.}

\work{Full-text}
{Token-based index for textual content that supports complex queries like inflectional forms, proximity searches, or thesaurus expansion. Powered by SQL Server Full-Text Engine.}

\work{Kursory}
{Cursors allow row-by-row processing of query results. Useful when set-based operations are not feasible. They are slower and more resource-intensive, so they should be used sparingly.}


\work{DOM Virtual DOM}
{
The DOM (Document Object Model) is a structure that represents an HTML document as a tree of nodes. The Virtual DOM is a lightweight copy of the real DOM, used mainly by libraries like React. Changes are first applied to the Virtual DOM, then the differences (diff) are efficiently updated in the real DOM, improving rendering performance.
}

\work{JS array methods}
{
JavaScript array methods such as map, filter, reduce, forEach, some, every, find, includes, sort, etc., allow for functional data manipulation. They're used to transform, filter, aggregate, and search arrays in a declarative way.
}

\work{TypeScript Type vs Interface}
{
interface defines the shape of an object and is commonly used for class or data contracts. type can represent unions (|), intersections (\&), and aliases for primitives or functions. They're often interchangeable, but type is more flexible, while interface is better for inheritance.
}

\work{React Hook}
{
Hooks are functions that let you use React features such as useState, useEffect, useContext without writing class components. They allow state management, side effects, and lifecycle logic within functional components.
}

\work{Promise}
{
A Promise is an object representing the eventual completion or failure of an asynchronous operation. It can be in pending, fulfilled, or rejected state. Methods like then, catch, and finally allow chaining operations. Widely used for async programming in JavaScript.
}

\work{Async Await}
{
Syntactic sugar for working with Promises. An async function always returns a Promise. await pauses execution until the Promise resolves or rejects. Makes asynchronous code more readable and easier to follow.
}

\work{REST}
{
REST (Representational State Transfer) is an architectural style for networked applications. It uses standard HTTP methods (GET, POST, PUT, DELETE, etc.), resources represented as URIs, and typically communicates using JSON/XML. REST APIs follow these principles.
}

\work{Get Post}
{
GET is used to retrieve data (should not have side effects), while POST is used to create new resources (usually with a request body). GET should be idempotent and safe; POST is not necessarily.
}

\work{Put vs Patch}
{
PUT sends a complete representation of a resource and replaces the existing one. PATCH only updates selected fields. PUT is idempotent (can be repeated safely), PATCH is not necessarily.
}

\work{CQRS}
{
Command Query Responsibility Segregation — separates reads (queries) from writes (commands) that change state. Enables better scalability, clear responsibility separation, and distinct data models for reads and writes.
}

\work{Outbox}
{
A pattern for reliable messaging in distributed systems. Instead of sending messages directly, they are stored in an Outbox table within the same transaction as domain changes. A separate process reads from the Outbox and publishes messages (e.g., to Kafka or RabbitMQ).
}

\work{projects}
{
A collection of real-life or demo applications that demonstrate the use of architectural patterns, technologies, and best practices. Projects help assess an engineer’s experience and skills.
}

\work{good/interesting things in projects}
{
Examples of good practices: applying Clean Architecture, CI/CD automation, proper unit/integration testing, monitoring, solid domain modeling, zero-downtime deployments. These show technical maturity of the system.
}

\work{bad things in projects and how to solve them}
{
Examples: no tests → introduce TDD or at least critical-path unit tests; poor separation of concerns → use CQRS; bad performance → profiling and query optimization or caching; hard-to-maintain code → refactoring, applying SOLID principles. It's important not only to identify issues but to propose viable solutions.
}

\header{C\#/ASP.NET}
\work{REST Methods}
{
Understand the purpose of each HTTP method (GET, POST, PUT, PATCH, DELETE) and when to use them in the context of RESTful APIs.
}

\work{HTTP Status Codes (Ok, NoContent, BadRequest, NotFound, InternalServerError) and When to Use Them}
{
Each status code represents the outcome of an HTTP request. Use 200 OK for successful responses with content, 204 No Content when there's no response body, 400 Bad Request for client-side errors, 404 Not Found when the resource doesn't exist, and 500 Internal Server Error for unhandled server exceptions.
}

\work{Middleware – How and When to Use It; Injecting Singleton, Scoped}
{
Middleware components process requests and responses in ASP.NET Core. Use singleton for shared state, scoped for per-request dependencies. Know how to inject them properly depending on their lifecycle.
}

\work{Dependency Injection (DI) Lifecycles}
{
Singleton: One instance for the entire app.

Scoped: One instance per request.

Transient: A new instance each time it's requested.
}

\work{Unit and Integration Testing}
{
Refer to official guidance: ASP.NET Core Integration Testing. Know how to isolate logic for unit tests and simulate the full request pipeline for integration tests.
}

\work{Interface Implementation}
{
Understanding how to implement interfaces and why it's essential for abstraction, testing, and dependency injection.
}

\work{Inheritance: override, virtual, abstract}
{
Understand inheritance principles, how to use virtual and override, and when to declare members as abstract.
}

\work{Generic Types}
{
Leverage generics for type-safe, reusable code. Understand how to declare, constrain, and use them effectively.
}

\work{Covariance and Contravariance}
{
See: Covariance and Contravariance
Important for working with collections and delegates where type compatibility matters.
}

\header{Entity Framework}
\work{DbContext (Object Scope)}
{
DbContext is scoped per request in ASP.NET by default. Avoid reusing it across threads or requests.
}

\work{Transactions (When and How to Use)}
{
Use explicit transactions with BeginTransaction when you need multiple operations to commit atomically.
}

\work{Materialization}
{
The process of converting raw data from the database into .NET objects (e.g., using ToList, First, etc.).
}

\work{Core LINQ Methods – Differences between First, Single, etc.}
{
First: Returns the first element or throws if none found.

FirstOrDefault: Returns first or default value (e.g., null).

Single: Expects exactly one element. Throws if more or less.

SingleOrDefault: Expects at most one. Returns default if none.
}

\work{When NOT to Use Entity Framework – Alternatives like Dapper}
{
In performance-critical scenarios (like bulk operations or fine-grained control over SQL), consider alternatives like Dapper. This is especially relevant in DDD where persistence logic might be separated.
}

\work{Code First vs. Model First}
{
Code First: Define entities in C\#, database is generated.

Model First: Design database visually, generate code from model.
}

\work{Basic DB Concepts: Views and Tables}
{
Table: Physical structure storing data.

View: Virtual table based on a query, useful for abstraction.
}

\header{Architecture}

\work{Monolith vs. Microservices – Pros and Cons}
{
Monolith: simpler deployment, tightly coupled.
Microservices: scalability, modularity, but introduces complexity in communication and data consistency.
}

\work{Clean Architecture}
{
Layers: Entities, Use Cases, Interface Adapters, Frameworks \& Drivers. Dependencies flow inward. Promotes separation of concerns and testability.
}

\work{Hexagonal Architecture (Ports and Adapters)}
{
Core business logic is decoupled from external inputs/outputs via interfaces (ports) and implementations (adapters).
}

\work{Request, Response, Query, Command, Event}
{
Request/Response: Synchronous interactions.

Query/Command: CQS separation – read vs write.

Event: Asynchronous notification of state change.
}

\work{DDD Tactical Patterns: Aggregate Root, Repository, Entity, Value Object, Domain Service, Bounded Context, Ubiquitous Language}
{
Understand each component and their role in modeling complex domains. Focus on enforcing invariants and proper boundaries.
}

\work{Outbox/Inbox Patterns}
{
Ensure reliable messaging by storing events in an ""outbox"" table and publishing them asynchronously. Inbox pattern prevents duplication on the consumer side.
}

\work{Command Query Separation (CQS)}
{
Each method should either change state or return data—but not both.
}

\work{Command Query Responsibility Segregation (CQRS)}
{
Separate read and write models for better scalability and flexibility. Concept popularized by Martin Fowler.
}

\work{Event-Driven Microservices}
{
Services communicate via events (e.g., Kafka, RabbitMQ). Promotes loose coupling but requires robust message handling and schema management.
}

\header{Extra}
\work{Challenges of Distributed Systems}
{
Issues like partial failures, network latency, eventual consistency, message ordering, and observability.
}

\work{Breaking Changes (When They Occur – DB, API, etc.)}
{
Avoid making changes that break existing contracts. Use versioning, backward-compatible migrations, and feature toggles.
}

\work{Distributed Transactions}
{
Complex and costly. Prefer eventual consistency and patterns like Saga or Outbox over two-phase commits.
}

\work{Saga Pattern}
{
Manage long-running business transactions with rollback/compensation logic instead of traditional DB transactions.
}

\work{Saga Pattern – Orchestration}
{
Central controller (orchestrator) directs the saga flow.
}

\work{Saga Pattern – Choreography}
{
Each service reacts to events and emits new ones. No central coordinator.
}

\work{Message Delivery Guarantees (e.g., Kafka)}
{
At-most-once: No retries; may lose messages.

At-least-once: Retries allowed; duplicates possible.

Exactly-once: Hard to achieve; Kafka supports it via idempotent producers and transactions.
}

\work{Zero-Downtime DB Migrations \& Backward Compatibility}
{
Strategies include: non-breaking schema changes, toggled deployments, shadow writes, and dual reads/writes during transition.
}

\work{Deployment Strategies}
{
Blue-Green, Canary Releases, Rolling Updates. Choose based on risk tolerance, rollback capabilities, and system complexity.
}

\work{Kubernetes}
{
Container orchestration platform. Supports scaling, self-healing, service discovery, and declarative configuration.
}

\header{Best Programming Practices}
\work{S in SOLID}
{Single Responsibility Principle (SRP) – A class should have only one reason to change. Keeps code focused and easier to maintain.}

\work{O in SOLID}
{Open/Closed Principle (OCP) – Software entities should be open for extension, but closed for modification. Extend via inheritance or composition.}

\work{L in SOLID}
{Liskov Substitution Principle (LSP) – Derived classes must be substitutable for their base types without altering the correctness of the program.}

\work{I in SOLID}
{Interface Segregation Principle (ISP) – Prefer many small, specific interfaces over large, general ones. Promotes flexible and maintainable code.}

\work{D in SOLID}
{Dependency Inversion Principle (DIP) – High-level modules should depend on abstractions, not on low-level modules. Use dependency injection to invert control.}

\work{DRY Principle}
{Don't Repeat Yourself – Avoid code duplication. Reuse logic via functions, classes, and services. Helps reduce bugs and improves maintainability.}

\work{Access Modifiers in C\#}
{public, private, protected, internal, protected internal, private protected, and file. Each controls visibility at class, assembly, or file level. Choose the narrowest scope necessary.}

\work{Properties vs. Fields in C\#}
{

Field: A direct variable inside a class.

Property: Provides controlled access to a private field through get/set.

Properties enable encapsulation, data binding, and abstraction. Fields are simpler but limited in flexibility.
}

\work{Can You Resize an Array at Runtime?}
{Arrays have fixed size. To ""resize"", you must create a new array and copy the contents using Array.Copy or Array.Resize. Prefer dynamic collections like List<T> for flexible sizing.}

\work{String Concatenation Methods in C\#}
{

+ operator – Simple, inefficient for many operations.

string.Concat – Joins multiple strings.

string.Join – Joins with a separator.

StringBuilder – Efficient for many modifications.

String interpolation (\$""Hello {name}"") – Readable syntax for constructing strings.
}

\work{Differences Between Class and Struct in C\#}
{

Type: Class = reference type; Struct = value type.

Memory: Classes on heap, structs on stack.

Inheritance: Classes support it, structs do not (except interfaces).

Performance: Structs are lightweight but copied by value.

Constructors: Structs had no parameterless constructors before C\# 10.

Nullability: Structs need Nullable<T> for null support.

Access modifiers: Structs don’t support protected.
}

\work{Commenting Guidelines in Code}
{Comments should be used sparingly and never as a substitute for clear naming of variables, functions, or classes. They should explain why the code is written in a certain way, not what it does.

Avoid obvious or overly detailed comments.

Keep comments up to date. Outdated or incorrect comments can be misleading.

Use comments to clarify complex or non-obvious code sections.

Use comments to document public APIs such as methods, properties, or classes—preferably using XML documentation comments.

Comments can also be used to mark areas needing further work, using tags like TODO, FIXME, or HACK.}

\work{Reading a Private Field from an Object}
{This is generally discouraged, but it can be done using reflection:

obj.GetType().GetField(""privateField"", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj)}

\work{Dictionary}
{Used when storing and manipulating key-value pairs.

Provides constant time lookup O(1) via hashing.

The internal data structure resizes dynamically as needed.

No inherent ordering of data.

Useful for mapping between related datasets.}

\work{sealed Keyword}
{When applied to a class, prevents it from being inherited. When applied to a method, prevents it from being overridden in derived classes.}

\work{?? Operator}
{The null-coalescing operator. Returns the value on the left if it's not null; otherwise, returns the right-hand side (default value).}

\work{is and as Keywords}
{

is: Checks if an object is of a specified type or is null (obj is null).

as: Attempts to cast an object to a given type; returns null if the cast fails. Safer than direct casting when nullability is acceptable.
}

\work{TransactionScope}
{A class in .NET (System.Transactions) that enables a group of operations to be executed within a transaction scope. Often used with databases or other transactional systems.

To start a scope, instantiate TransactionScope. All operations within it are considered part of a transaction.
You must call Complete() to commit; otherwise, the transaction is automatically rolled back.

It simplifies error handling and ensures data consistency across multiple operations.}

\work{Why is virtual/override Better than new?}
{Using virtual/override supports polymorphism, enabling correct behavior during runtime dispatch. It provides greater control and flexibility over method inheritance and extension.
In contrast, new hides the base method without polymorphic behavior, which can lead to unexpected results.}

\work{How to Prevent UI Freezing in WinForms During Long Operations}
{Use BackgroundWorker, async/await, or Dispatcher (in WPF) to offload long-running tasks from the UI thread and maintain responsiveness.}

\work{async / await Keywords}
{async marks a method as asynchronous, and await is used before an asynchronous call to indicate that the calling thread should pause execution until the awaited task completes—without blocking the thread. Enables responsive UI and scalable server code.}

\work{Race Condition}
{A concurrency issue where the behavior of a program depends on the timing or sequence of thread execution when accessing shared data. Improper synchronization may cause unpredictable results or bugs due to simultaneous modifications.}

\work{Semaphores}
{A semaphore limits the number of threads that can access a resource concurrently. Initialized with a count representing available slots.

Use Wait() to attempt acquiring the semaphore. If the count is > 0, access is granted and count is decremented.

If the count is 0, the thread is blocked until another thread calls Release(), which increments the count and unblocks waiting threads.

Useful for controlling access to finite resources like database connections or thread pools.}

\work{Code-Behind}
{Refers to the file where the logic for UI elements (e.g., event handlers) is implemented—typically in WinForms, WPF, or ASP.NET. In MVVM/MVC patterns, the code-behind handles user interaction logic and should ideally remain thin, delegating business logic elsewhere.}

\work{How Does the yield Keyword Work?}
{Used to define iterators that lazily return elements one at a time. Instead of returning a full collection, yield return pauses execution and resumes when the next element is requested. Enables efficient, memory-friendly iteration over sequences.}

\work{Which database engines have you worked with?}
{Oracle SQL, Transact-SQL}

\work{What is a dblink?}
{A database link (dblink) is a schema object in a database that allows access to objects on a remote database. It enables queries, DML operations, and even PL/SQL execution across different databases as if they were local. Commonly used in Oracle to facilitate distributed systems.}

\work{What is a database transaction?}
{A database transaction is a unit of work that is executed in an all-or-nothing manner. It must meet the ACID properties:
\begin{itemize}
  \item \textbf{Atomicity} – the transaction is all or nothing.
  \item \textbf{Consistency} – ensures that the database remains in a valid state.
  \item \textbf{Isolation} – prevents concurrent transactions from interfering with each other.
  \item \textbf{Durability} – changes are permanent once committed.
\end{itemize}}

\work{What isolation levels can you name?}
{
\begin{itemize}
  \item \textbf{Read Uncommitted} – Allows reading uncommitted changes, leading to dirty, non-repeatable, or phantom reads.
  \item \textbf{Read Committed} – Prevents dirty reads, but still allows non-repeatable and phantom reads.
  \item \textbf{Repeatable Read} – Prevents dirty and non-repeatable reads but allows phantom reads.
  \item \textbf{Serializable} – Highest level, prevents all anomalies by executing transactions in full isolation.
  \item \textbf{Snapshot} – Uses data versioning to provide consistent reads without locking, avoiding many concurrency issues.
\end{itemize}}

\work{What is a dirty read?}
{A dirty read occurs when a transaction reads data that has been modified by another transaction but not yet committed. If the other transaction is rolled back, the read data becomes invalid, causing inconsistencies.}

\work{What is a phantom read?}
{A phantom read happens when, during a single transaction, a query returns different results due to another transaction inserting or deleting rows that match the query condition. This leads to inconsistent results within the same transaction.}

\work{What is a package in Oracle?}
{A package in Oracle is a schema object that groups related PL/SQL types, procedures, functions, and variables into a single unit. It provides modularity, better performance (due to loading once into memory), and encapsulation of logic.}

\work{What are the differences between git fetch and git pull?}
{
\begin{itemize}
  \item \textbf{Fetch} downloads changes from the remote repository but does not integrate them into the current branch.
  \item \textbf{Pull} performs a fetch followed by a merge (or rebase) into the current branch, updating your working copy.
\end{itemize}}

\work{What are the differences between merge and rebase?}
{
\begin{itemize}
  \item \textbf{Merge} combines branches and creates a new commit in the history.
  \item \textbf{Rebase} re-applies commits from one branch onto another, rewriting history to maintain a linear commit structure.
\end{itemize}}

\work{What branching strategies do you know?}
{
\begin{itemize}
  \item \textbf{Feature Branching} – separate branch per feature.
  \item \textbf{Gitflow} – structured model using branches for master, develop, features, releases, and hotfixes.
  \item \textbf{GitHub Flow} – simplified flow where features are merged directly into the main branch with tagging for releases.
  \item \textbf{Trunk-Based Development} – all developers work on a single main branch with short-lived feature branches, supported by CI/CD.
\end{itemize}}

\work{What is REST?}
{Representational State Transfer is a software architectural style used for designing networked applications. It relies on standard HTTP methods and emphasizes stateless communication, caching, and a uniform interface for scalable, maintainable APIs.}

\work{What is WSDL?}
{Web Services Description Language is an XML-based language used to describe the functionality offered by a web service using SOAP. It defines the service’s operations, messages, bindings, and location, allowing tools to generate client code automatically.}

\work{What is IL (Intermediate Language)?}
{Intermediate Language is the low-level language into which .NET source code (C\#, F\#, VB.NET, etc.) is compiled. At runtime, the Common Language Runtime (CLR) compiles IL into native machine code for execution on the target platform.}

\work{What are the differences between .NET Framework, .NET Core, .NET Standard, and .NET?}
{
\begin{itemize}
  \item \textbf{.NET Framework} – the original Windows-only implementation of .NET for desktop and web apps.
  \item \textbf{.NET Core} – a cross-platform, high-performance, open-source version of .NET. Deprecated in favor of .NET 5+.
  \item \textbf{.NET (5+)} – the unified platform combining .NET Core and Xamarin, now the main development model.
  \item \textbf{.NET Standard} – a specification that defines APIs available across all .NET implementations for maximum compatibility.
  \item \textbf{Mono} – a cross-platform .NET implementation primarily used in game development (e.g., Unity).
\end{itemize}}

\work{Describe design patterns}
{
\vspace{0.4cm}

\textbf{Singleton}: Ensures a class has only one instance and provides a global point of access to it.

\vspace{0.4cm}

\textbf{Factory Method}: Defines an interface for creating objects, but lets subclasses decide which class to instantiate.

\vspace{0.4cm}

\textbf{Abstract Factory}: Provides an interface to create families of related or dependent objects without specifying their concrete classes.

\vspace{0.4cm}

\textbf{Prototype}: Creates new objects by cloning existing ones. Useful when object creation is costly.

\vspace{0.4cm}

\textbf{Builder}: Separates the construction of a complex object from its representation, allowing the same construction process to create different representations.

\vspace{0.4cm}

\textbf{Adapter}: Converts one interface to another expected by the client.

\vspace{0.4cm}

\textbf{Bridge}: Decouples an abstraction from its implementation, allowing both to evolve independently.

\vspace{0.4cm}

\textbf{Composite}: Treats individual objects and compositions of objects uniformly.

\vspace{0.4cm}

\textbf{Decorator}: Dynamically adds behavior or responsibilities to objects without altering their structure.

\vspace{0.4cm}

\textbf{Facade}: Provides a simplified interface to a complex subsystem.

\vspace{0.4cm}

\textbf{Flyweight}: Reduces memory usage by sharing as much data as possible between similar objects.

\vspace{0.4cm}

\textbf{Proxy}: Provides a surrogate or placeholder for another object to control access to it.

\vspace{0.4cm}

\textbf{Chain of Responsibility}: Passes a request along a chain of handlers until one of them handles it.

\vspace{0.4cm}

\textbf{Command}: Encapsulates a request as an object, allowing for parameterization and queuing.

\vspace{0.4cm}

\textbf{Iterator}: Provides a way to access elements of a collection sequentially without exposing its structure.

\vspace{0.4cm}

\textbf{Mediator}: Centralizes complex communications and control logic between objects.

\vspace{0.4cm}

\textbf{Memento}: Captures and externalizes an object's internal state so it can be restored later without violating encapsulation.

\vspace{0.4cm}

\textbf{Observer}: Defines a one-to-many dependency so that when one object changes state, all its dependents are notified.

\vspace{0.4cm}

\textbf{State}: Allows an object to change its behavior when its internal state changes.

\vspace{0.4cm}

\textbf{Strategy}: Defines a family of algorithms, encapsulates each one, and makes them interchangeable.

\vspace{0.4cm}

\textbf{Template Method}: Defines the skeleton of an algorithm in a base class, deferring specific steps to subclasses.
}

\work{Libraries supporting MVVM}
{Model: represents the application's data and business logic.

View: responsible for presenting data to the user and handling user interactions.

ViewModel: acts as a mediator between the Model and the View, transforming data from the Model into a form suitable for the View and relaying commands from the View to the Model.

MVVM promotes separation of concerns between business logic and presentation, making the application easier to test and maintain.

Common MVVM libraries:

Prism: a popular open-source library for WPF, Xamarin.Forms, and UWP. Provides modules, navigation, commands, and event aggregation.

MVVM Light Toolkit: a lightweight open-source library for WPF, Silverlight, Xamarin, and UWP. Offers core MVVM functionality such as commands, ViewModel messaging, and dialog/navigation services.

Caliburn.Micro: an open-source library for WPF, Silverlight, Xamarin, and UWP. Facilitates MVVM application development through naming conventions and advanced data binding.

Note: WPF and Xamarin.Forms natively support data binding, enabling MVVM without external libraries. These libraries provide additional tooling to streamline development.}

\work{Dependency Injection (DI) libraries}
{Microsoft.Extensions.DependencyInjection, Autofac, Unity, Castle Windsor, StructureMap, Ninject, Simple Injector}

\work{DI Containers}
{Tools that manage dependencies by automatically creating and injecting object instances along with their dependencies.}

\work{Object lifetimes in DI}
{Transient -- a new instance is created each time it is requested.

Scoped -- a single instance is shared within a defined scope (e.g., a web request).

Singleton -- a single shared instance is used for the entire application lifetime.}

\work{What is Middleware and when is it useful (ASP.NET Core)?}
{Middleware are components that participate in handling HTTP requests and responses in ASP.NET Core applications. They are responsible for tasks such as logging, authentication, authorization, error handling, and response compression.

Middleware forms a request pipeline, where each component can inspect, modify, or short-circuit the request before passing it on. Eventually, the request reaches an endpoint (e.g., controller), and the response flows back through the middleware pipeline, where components can again process it.

Middleware is particularly useful for:
- Logging
- Authentication and Authorization
- Error handling
- Compression
- Caching}

\work{Razor vs Blazor}
{Razor is a templating engine for creating dynamic HTML views in ASP.NET applications. It allows embedding C\# directly within HTML and is often used in MVC and Razor Pages.

Blazor is a .NET-based framework for building interactive web UIs using C\# instead of JavaScript. It supports two hosting models:
- Blazor Server: C\# code runs on the server, and UI updates are sent via SignalR.
- Blazor WebAssembly: C\# code is compiled to WebAssembly and runs in the browser.

Blazor enables full-stack .NET development for web applications.}

\work{What are HTTP methods?}
{GET -- retrieves data from the server. Safe and idempotent.

POST -- submits data to the server to create a new resource. Not safe nor idempotent.

PUT -- updates an existing resource. Idempotent.

PATCH -- partially updates a resource. Idempotent.

DELETE -- removes a resource. Idempotent.

HEAD -- retrieves response headers without the body. Useful for checking resource availability or metadata.

OPTIONS -- returns allowed communication options and HTTP methods for a resource.

CONNECT -- used to establish a tunnel to the server, typically for SSL/TLS.

TRACE -- returns the received request for diagnostic purposes. Rarely used and often disabled for security.}

\work{What is MinimalAPI?}
{A simplified and declarative way to define HTTP endpoints in ASP.NET Core, minimizing boilerplate code and startup configuration. Ideal for lightweight services and microservices.}

\work{How to handle environment-specific configuration (production vs development)?}
{
Use the `ASPNETCORE\_ENVIRONMENT` environment variable set to `Development`, `Production`, etc.

Provide separate configuration files:
- `appSettings.Development.json`
- `appSettings.Production.json`
}

\work{Tools for testing Web APIs}
{Postman -- versatile API testing and documentation tool.

Insomnia -- lightweight alternative to Postman.

SoapUI -- testing tool for both SOAP and REST APIs.

Fiddler -- HTTP debugging proxy useful for inspecting network traffic.

RestClient (VS Code extension) -- allows in-editor API requests.

curl -- command-line tool for making HTTP requests.

JMeter -- Java-based tool for automated performance and API testing. Supports scripting, assertions, and reporting.}

\work{Span<T> and Memory<T> in .NET}{
`Span<T>` is a stack-only, high-performance type used for slicing memory without allocations. `Memory<T>` is a heap-allocated alternative that can be used asynchronously. Both provide safe ways to work with buffers, strings, or arrays and are ideal for performance-critical applications like parsers, serializers, or network protocols.
}

\work{Nullable Reference Types}{
Introduced in C\# 8.0, nullable reference types help prevent `NullReferenceException` by making nullability explicit in reference types. Use `?` to annotate nullable references and enable compiler warnings when possible null usage is detected. It improves code clarity and static safety. Enable this feature in `.csproj` with `<Nullable>enable</Nullable>`.
}

\work{Source Generators in .NET}{
Source generators run at compile time to generate C\# code. They are useful for reducing boilerplate (e.g., INotifyPropertyChanged), improving performance, or generating serialization logic. They operate on syntax trees and metadata, allowing advanced metaprogramming while maintaining runtime performance.
}

\work{Reflection and Emit in .NET}{
Reflection enables inspecting and interacting with metadata about assemblies, types, and members at runtime. `System.Reflection.Emit` allows creating new types and methods dynamically. It's used in advanced scenarios like ORMs (e.g., EF Core), dynamic proxies, or AOP tools. Be cautious with performance and security implications.
}

\work{Working with Expression Trees}{
Expression trees represent code in a tree-like data structure. They are used by LINQ providers to convert C\# code into SQL or other formats. Developers can use `Expression<Func<T, bool>>` to build and manipulate expressions at runtime. Expressions can be compiled into delegates for execution.
}

\work{Task Parallel Library (TPL)}{
TPL provides the `Task` class and related constructs for efficient parallelism and asynchrony. Use `Parallel.For`, `Parallel LINQ`, or `Task.Run` to perform work in parallel. TPL abstracts thread management and improves scalability over traditional threads. Prefer async/await for IO-bound tasks and TPL for CPU-bound operations.
}

\work{SynchronizationContext vs. TaskScheduler}{
`SynchronizationContext` controls how async continuations are executed (e.g., UI thread, ASP.NET context). `TaskScheduler` manages how and where `Task`s run. Custom schedulers allow priority-based or serialized task execution. Understanding these is critical for avoiding deadlocks or race conditions, especially in UI and server environments.
}

\work{Deadlocks and How to Avoid Them}{
A deadlock occurs when threads wait on each other’s resources indefinitely. Common causes include improper lock order and holding locks while calling external code. Avoid nested locks, use timeout-based lock attempts (`Monitor.TryEnter`), and prefer async/await over manual locking to reduce risk.
}

\work{Memory Leaks in .NET Applications}{
Even with GC, memory leaks can happen due to event handlers, static references, and long-lived object graphs. Use tools like dotMemory, ANTS Memory Profiler, or Visual Studio Diagnostic Tools to identify leaks. Properly unsubscribe from events and avoid large object retention in static caches.
}

\work{AOT (Ahead-of-Time) Compilation in .NET}{
AOT compiles .NET code into native binaries before runtime, reducing startup time and memory usage. Useful in scenarios like Blazor WebAssembly, mobile (Xamarin/MAUI), and constrained environments. .NET 7+ supports NativeAOT for trimming and performance optimization. Tradeoffs include longer build times and limited reflection support.
}

\work{gRPC vs REST in .NET}{
gRPC is a high-performance, binary protocol based on HTTP/2, suitable for internal microservices with strict contracts and low latency. REST is more flexible, human-readable (JSON), and suitable for external/public APIs. gRPC uses `.proto` files and offers code generation, while REST is schema-less.
}

\work{SignalR for Real-time Communication}{
SignalR enables server-client bi-directional communication, ideal for chat apps, live dashboards, or notifications. It abstracts WebSockets, long polling, and Server-Sent Events. It integrates seamlessly into ASP.NET Core and supports groups, user connections, and hub-based communication.
}

\work{Custom Model Binding in ASP.NET Core}{
Custom model binders handle non-standard input (e.g., from headers, route data, or complex formats). Implement `IModelBinder` and register it in `Startup.cs`. Useful when binding enums, special types, or nested objects from request bodies or query strings.
}

\work{Security Best Practices in ASP.NET Core}{
Secure ASP.NET Core apps with HTTPS redirection, data protection APIs, authentication/authorization middleware, and secure headers. Protect against XSS with Razor encoding, CSRF with anti-forgery tokens, and use Identity for user management. External providers (OAuth, OpenID Connect) can be integrated via middleware.
}

\work{OpenAPI/Swagger in ASP.NET Core}{
Swagger (via Swashbuckle) auto-generates interactive API docs. Customize it with XML comments, filters, and annotations. Use NSwag for client generation (TypeScript, C\#). Ensure security schemes (JWT, OAuth) are documented for testing protected endpoints.
}

\work{Rate Limiting in Web APIs}{
Prevent abuse or DoS attacks by implementing rate limiting. Use libraries like AspNetCoreRateLimit or built-in .NET 8 middleware. Strategies include fixed window, sliding window, and token bucket. Apply limits per IP, API key, or user identity.
}

\work{Health Checks in ASP.NET Core}{
Health checks monitor application status (e.g., DB connectivity, service availability). Use `AddHealthChecks()` in `Startup.cs`, expose endpoints (`/health`), and integrate with orchestration tools like Kubernetes or Azure. Create custom checks via `IHealthCheck`.
}

\work{Event Sourcing}{
Event sourcing stores state changes as a sequence of events instead of current state. It enables audit trails, temporal queries, and complex undo scenarios. Requires careful event design and can increase system complexity. Often paired with CQRS and message brokers.
}

\work{CAP Theorem}{
The CAP theorem states that in a distributed system, only two of Consistency, Availability, and Partition Tolerance can be guaranteed. You must choose trade-offs based on requirements (e.g., AP for eventual consistency systems, CP for strict data integrity).
}

\work{Resilient Communication in Microservices}{
Use retry, fallback, circuit breakers, and timeout policies to make services resilient. Polly is a popular .NET library supporting these patterns. Prevent cascading failures by isolating failing dependencies and employing bulkhead isolation strategies.
}

\work{Caching Strategies in .NET}{
Use in-memory caching (`IMemoryCache`) for local caching, or distributed caching (Redis, SQL) for shared environments. Choose absolute/relative expiration or sliding windows. Carefully manage cache invalidation. Use output caching or response caching where applicable.
}

\work{Testing Middleware in ASP.NET Core}{
Test middleware by creating a minimal pipeline using `TestServer` or `WebApplicationFactory`. Inject mock services or headers and assert responses. Custom middleware should be isolated and tested for expected HTTP pipeline behavior.
}

\work{What is the difference between `ConfigureServices` and `Configure` in ASP.NET Core?}{
`ConfigureServices(IServiceCollection)` is used to register dependencies (DI), options, and services. `Configure(IApplicationBuilder)` builds the HTTP request pipeline (middleware, routing, endpoints). They are part of the startup lifecycle and are essential for setting up app behavior.
}

\work{What is the difference between Task and Thread in .NET?}{
A `Thread` is a low-level unit of execution directly managed by the OS. A `Task` is a higher-level abstraction provided by the Task Parallel Library (TPL) that uses the thread pool and supports better scalability through asynchronous programming. `Task` allows continuations, cancellation, exception handling, and easier composition of asynchronous workflows.
}

\work{What is the difference between async/await and Task.Run()?}{
`async/await` is a language feature for asynchronous programming that helps avoid blocking threads by yielding control back to the caller. `Task.Run()` is used to execute CPU-bound work on a thread pool thread. Prefer `async/await` for I/O-bound operations and `Task.Run()` for CPU-bound work when you want to offload to a background thread.
}

\work{What is Span<T> and Memory<T>?}{
`Span<T>` is a stack-only, type-safe representation of a contiguous region of arbitrary memory. It enables high-performance memory manipulation without allocations. `Memory<T>` provides similar functionality but is heap-allocatable and usable across `async` calls. These are essential for performance-critical scenarios.
}

\work{What is the difference between IEnumerable and IQueryable?}{
`IEnumerable` is used for in-memory iteration and LINQ-to-Objects. `IQueryable` enables query composition and deferred execution, typically translated to SQL or other query languages via LINQ providers (like Entity Framework). Use `IQueryable` for database access and `IEnumerable` for in-memory collections.
}

\work{What are Nullable Reference Types (NRTs)?}{
Introduced in C\# 8, nullable reference types help catch null-related bugs at compile time. By enabling NRTs, you can explicitly annotate reference types as nullable (`string?`) or non-nullable (`string`). The compiler will warn if a nullable value is dereferenced without a null check.
}

\work{What is a CancellationToken in .NET?}{
`CancellationToken` is used to signal cancellation in asynchronous or long-running operations. You pass a `CancellationToken` to methods that support cancellation. This allows cooperative cancellation and ensures responsive and cancellable applications.
}

\work{What is the difference between ref, out, and in parameters?}{
- `ref` allows both reading and writing to the original variable.
- `out` requires the called method to assign a value before returning.
- `in` is a read-only reference, used for performance to avoid copying large structs.
They are primarily used for performance-sensitive or legacy interop scenarios.
}

\work{What is Reflection in .NET?}{
Reflection allows runtime inspection of types, methods, properties, and attributes. It’s useful for dynamic loading, plugin systems, and serialization. However, it comes with performance overhead and should be used judiciously. Libraries like `System.Reflection` and `System.Reflection.Emit` provide access to metadata and IL.
}

\work{What is the difference between strong and weak references?}{
A strong reference prevents the object from being garbage collected. A weak reference (`WeakReference<T>`) allows the object to be collected while still being trackable. Weak references are useful in caching scenarios to avoid memory leaks.
}

\work{What are default interface methods in C\#?}{
Introduced in C\# 8.0, default interface methods allow method implementations in interfaces. This enables versioning of interfaces without breaking existing implementations. However, overuse can lead to confusion and violates traditional interface purity.
}

\work{What is boxing and unboxing in .NET?}{
Boxing is the process of converting a value type (like `int`) to a reference type (`object`). Unboxing is the reverse. Boxing incurs performance overhead and should be minimized in performance-critical code, especially in loops.
}

\work{What is the difference between const, readonly, and static readonly?}{
- `const`: compile-time constant; must be primitive or string.
- `readonly`: value is assigned at construction and immutable thereafter.
- `static readonly`: same as `readonly`, but applies to static members.
Use `const` for global constants, and `readonly` for runtime-initialized constants.
}

\work{What is a record in C\#?}{
A `record` is a reference type introduced in C\# 9 for immutable data modeling. Records provide value-based equality, concise syntax, and deconstruction. Ideal for DDD value objects or DTOs. Use `record` for immutability and data-centric scenarios.
}

\work{What is the purpose of the using statement and IAsyncDisposable?}{
The `using` statement ensures that an object implementing `IDisposable` is disposed properly, releasing unmanaged resources. In C\# 8.0 and later, `await using` supports `IAsyncDisposable`, which allows asynchronous cleanup (e.g., flushing an async stream).
}

\work{What are top-level statements in C\#?}{
Top-level statements allow you to write C\# code outside a `Main` method. This is commonly used in console applications and minimal APIs. Improves code readability by removing ceremony, especially for small scripts or microservices.
}

\work{What are Minimal APIs in ASP.NET Core?}{
Minimal APIs provide a lightweight syntax for building HTTP APIs with minimal dependencies and code. They are defined using top-level statements and are ideal for microservices and small web applications. Use `.MapGet()`, `.MapPost()`, etc., directly in `Program.cs`.
}

\work{What is the difference between .NET Framework, .NET Core, and .NET (5+)?}{
- `.NET Framework`: Windows-only legacy platform.
- `.NET Core`: Cross-platform, modular, and performant redesign of .NET.
- `.NET` (5 and above): Unifies Core and Framework into a single platform with cross-platform support, performance enhancements, and modern APIs.
Use `.NET` (6/7/8) for all new development.
}

\work{What is AOT (Ahead-of-Time) Compilation in .NET?}{
AOT compiles IL into native code at publish time. Reduces startup time, improves performance, and is used in environments like mobile, WASM, and containers. .NET Native, NativeAOT, and ReadyToRun are examples. Suitable for high-performance and size-constrained apps.
}

\work{What is the difference between HttpClient and IHttpClientFactory?}{
`HttpClient` should not be created per request because it can exhaust sockets. `IHttpClientFactory` (introduced in ASP.NET Core 2.1) manages `HttpClient` lifetimes, avoids common pitfalls, supports named and typed clients, and integrates with Polly for resilience.
}


\pagebreak


\header{WPF}

\work{DependencyProperty vs. CLR Property}{
A `DependencyProperty` is a special type of property used in WPF for advanced features like data binding, animation, and styling. It supports value inheritance, change notification, default values, and property value expressions. CLR properties lack these capabilities. Define a `DependencyProperty` using `DependencyProperty.Register`, and typically wrap it in a CLR property for access.
}

\work{INotifyPropertyChanged and Data Binding}{
INotifyPropertyChanged is the interface used to notify the WPF binding system that a property value has changed. It is essential for implementing two-way data binding in MVVM. Implement this interface by raising `PropertyChanged` in the setter of bound properties.
}

\work{What is the Visual Tree and Logical Tree in WPF?}{
The logical tree represents the structure of UI elements from a XAML perspective, used in data binding and resource lookup. The visual tree includes all rendered elements, including templates and visual components. Tools like `VisualTreeHelper` allow you to inspect the visual tree.
}

\work{DataTemplates and ControlTemplates}{
`DataTemplate` defines how data objects are visually represented (e.g., in a `ListBox`). `ControlTemplate` defines how a control looks internally (e.g., styling a `Button`). Both are crucial for WPF UI customization and support complete UI separation via XAML.
}

\work{Value Converters in WPF}{
A value converter implements `IValueConverter` and transforms bound data between source and target formats. Useful for formatting, boolean-to-visibility conversions, or conditionally rendering UI. Custom converters can be defined in XAML and reused across bindings.
}

\work{Commanding and RoutedCommands}{
WPF uses commanding to decouple UI actions (buttons, menu items) from logic. `ICommand` and `RoutedCommand` support this. RoutedCommands also propagate up/down the visual tree. Use `RelayCommand` or `DelegateCommand` implementations in MVVM to bind logic to the ViewModel.
}

\work{Binding Modes in WPF}{
Binding supports modes like `OneTime`, `OneWay`, `TwoWay`, and `OneWayToSource`. Choose based on direction of data flow. For example, `TwoWay` is needed for editable fields, while `OneWay` suffices for read-only displays.
}

\work{Bubbling, Tunneling, and Direct Events}{
WPF event routing supports three strategies:
- **Bubbling**: Event moves up the visual tree (e.g., `Button.Click`).
- **Tunneling**: Prefixed with `Preview` and goes down the tree (`PreviewMouseDown`).
- **Direct**: Handled by the source only. Choose based on interception or central handling needs.
}

\work{Resource Dictionaries and MergedDictionaries}{
Resource dictionaries hold styles, templates, and brushes. Use `MergedDictionaries` to combine resources across files and libraries. This supports modular UI design and theming. Load resources dynamically for runtime theming.
}

\work{StaticResource vs. DynamicResource}{
`StaticResource` is resolved at compile/load time and is faster. `DynamicResource` is resolved at runtime and allows resources to change dynamically (e.g., switching themes). Use `DynamicResource` when values may change during application lifetime.
}

\work{MVVM in WPF}{
Model-View-ViewModel (MVVM) is the architectural pattern in WPF that promotes testability and separation of concerns. ViewModel exposes bindable properties and commands, View is purely XAML/UI, and Model represents the domain logic. Libraries like Prism, MVVM Light, and ReactiveUI support MVVM practices.
}

\work{Virtualization in WPF Controls}{
Controls like `ListBox` and `DataGrid` use virtualization to improve performance when displaying large data sets. Virtualization means UI elements are created only for visible data. `VirtualizingStackPanel` is the panel responsible for this in items controls.
}

\work{Dispatcher and UI Threading}{
WPF uses a single UI thread. Use `Dispatcher.Invoke` or `Dispatcher.BeginInvoke` to execute code on the UI thread from background threads. Essential for thread-safe UI updates. Avoid long-running work on the UI thread to keep the UI responsive.
}

\work{Attached Properties in WPF}{
Attached properties allow child elements to store values defined by parent types. For example, `Grid.Row` is an attached property defined by `Grid`. Declare with `DependencyProperty.RegisterAttached`. Useful for layouts and behaviors.
}

\work{Behaviors and Triggers}{
WPF supports Triggers (Property, Event, DataTriggers) to change UI based on property values. Behaviors (in Blend SDK or community libraries) attach reusable logic to elements without code-behind. Great for MVVM-friendly interactivity.
}

\work{ObservableCollection vs List}{
`ObservableCollection<T>` raises change notifications used by WPF's data binding engine, so the UI updates automatically when items are added/removed. `List<T>` does not raise such events and should not be used for dynamic collections in UI.
}

\work{Custom Controls vs. UserControls}{
`UserControl` is a simple composite control composed of existing controls. `CustomControl` derives from `Control` and is used when full templating and reuse is needed. Custom controls are more flexible and used for library-grade components.
}

\work{How to Prevent Memory Leaks in WPF}{
Common causes of memory leaks include event handlers not unsubscribed, long-lived static references, and retained visual elements. Use weak event patterns (`WeakEventManager`), unhook event handlers in `Dispose`, and analyze with tools like `dotMemory` or `Snoop`.
}

\work{How to Use Data Validation in WPF}{
Use `IDataErrorInfo` or `INotifyDataErrorInfo` to validate input in ViewModel. Binding engine reads error info and displays validation feedback. Combine with ValidationRules or `Validation.Errors` in XAML to show UI hints.
}

\work{Performance Optimization in WPF}{
To improve performance:
- Use virtualization for large item collections.
- Avoid nested panels.
- Freeze `Freezable` objects (like Brushes).
- Use `DeferredScrolling` and `Container Recycling`.
- Avoid using `DynamicResource` where not necessary.
}


\header{Memory Management}

\work{Garbage Collector}
{A memory management mechanism in .NET that automatically reclaims memory occupied by objects that are no longer in use. The GC identifies unreferenced objects, deallocates their memory, and may optionally compact memory. It uses a generational approach (Gen 0, 1, 2) to optimize performance. However, large GC operations can impact application responsiveness.}

\work{Memory Management in .NET}{
.NET uses a managed memory model, relying on a garbage collector (GC) to automatically reclaim unused objects. It organizes memory into generations (Gen 0, 1, 2) to optimize short-lived and long-lived object collection. Finalizers allow cleanup before GC, but IDisposable and the Dispose pattern should be used to deterministically release unmanaged resources. Use `using` statements or implement the dispose pattern with `IDisposable` to handle resource cleanup safely.
}

\work{What is the purpose of GC.Collect()? Should you use it?}{
`GC.Collect()` forces a garbage collection, which can degrade performance if misused. In most cases, you should **not** call it manually — the .NET GC is optimized to run automatically. Use it only in very specific, controlled scenarios (e.g., after large object deallocation in a memory-constrained environment).
}

\work{Tracing Garbage Collection}{
a form of automatic memory management that consists of determining which objects should be deallocated (""garbage collected"") by tracing which objects are reachable by a chain of references from certain ""root"" objects, and considering the rest as ""garbage"" and collecting them. Tracing is the most common type of garbage collection – so much so that ""garbage collection"" often refers to the tracing method.
}

\work{Automatic Reference Counting}{

}

\work{Description of CLR and GC}{
Garbage Collector (GC) w platformie .NET jest w pełni automatycznym systemem zarządzania pamięcią. Odpowiada nie tylko za usuwanie nieużywanych obiektów, ale również za alokację pamięci, kompaktowanie (defragmentację) oraz zarządzanie wirtualną przestrzenią adresową. Dzięki temu aplikacje w .NET nie muszą ręcznie zwalniać pamięci, co znacząco redukuje ryzyko błędów takich jak memory leaks czy dangling pointers.

Podczas tworzenia nowego obiektu na stercie (heap), .NET GC wykorzystuje ciągły wskaźnik przydziału (allocation pointer) w obrębie zaalokowanego segmentu pamięci. Alokacja polega po prostu na przesunięciu wskaźnika o rozmiar obiektu – bez potrzeby wyszukiwania wolnego miejsca. Dzięki temu proces alokacji w środowisku z GC może być szybszy niż w językach z manualnym zarządzaniem pamięcią.

Sterta zarządzana (managed heap) znajduje się w wirtualnej przestrzeni adresowej procesu. Ta przestrzeń pełni rolę warstwy pośredniej (ang. abstraction layer) między aplikacją a rzeczywistą pamięcią fizyczną (RAM, plik stronicowania, pamięć dyskowa).
Każdy obszar pamięci w tej przestrzeni może znajdować się w jednym z trzech stanów:

\begin{enumerate}
    \item Free – nieużywany, dostępny do rezerwacji,
    \item Reserved – zarezerwowany logicznie przez proces, ale jeszcze bez przypisanej fizycznej pamięci,
    \item Committed – zarówno zarezerwowany, jak i powiązany z fizyczną pamięcią RAM lub plikiem wymiany.
\end{enumerate}

GC i alokator zarządzają tymi stanami dynamicznie – system rezerwuje pamięć partiami (segmentami), które później są wykorzystywane przez kolejne generacje sterty.

.NET Garbage Collector stosuje podejście generacyjne (Generational Garbage Collection), oparte na obserwacji, że większość obiektów w aplikacjach jest krótkotrwała (tzw. „infant mortality phenomenon”). Dlatego sterta jest podzielona na trzy główne generacje:

\vspace{0.5cm}

\begin{center}
\begin{tabular}{llp{10cm}}
\textbf{Generacja}   & \textbf{Przeznaczenie} & \textbf{Opis} \\
Generacja 0	& Najmłodsze obiekty & Nowo utworzone obiekty, które zwykle szybko tracą ważność (np. obiekty tymczasowe, alokacje wewnątrz metod).\\
Generacja 1 & Obiekty średniożyjące & Obiekty, które przetrwały co najmniej jedno czyszczenie generacji 0, np. dane utrzymywane przez dłuższy czas w obrębie danej operacji. \\
Generacja 2 & Najstarsze obiekty & Długowieczne dane, np. statyczne pola, cache aplikacji, konfiguracja, singletony. \\
\end{tabular}
\end{center}

\vspace{0.5cm}

Dodatkowo istnieje Large Object Heap (LOH) – specjalny obszar pamięci przeznaczony dla dużych obiektów (domyślnie > 85 000 bajtów). LOH jest logicznie częścią generacji 2, ale jego czyszczenie i kompresja są obsługiwane oddzielnie. Do .NET 4.5 LOH nie był w ogóle defragmentowany; od .NET 5 wzwyż może być opcjonalnie kompresowany (np. w trybie serwerowym).

GC w .NET jest typu tracing, co oznacza, że działa w kilku etapach:

\begin{enumerate}
    \item Inicjacja:
GC uruchamia się automatycznie, gdy system uzna, że aplikacja zużyła zbyt dużo pamięci lub gdy zabraknie wolnego miejsca w danym segmencie sterty. Decyzja jest oparta na wielu czynnikach — m.in. dostępnej pamięci fizycznej, częstotliwości alokacji, aktywności procesora i trybie GC (Workstation, Server, Background).

    \item Root Enumeration (znalezienie korzeni):
GC rozpoczyna od zestawu rootów — to są wszystkie aktywne referencje: zmienne lokalne na stosie, wskaźniki CPU, rejestry, statyczne pola klas oraz uchwyty do obiektów przekazanych do natywnego kodu (P/Invoke, COM).

    \item Tracing (śledzenie grafu obiektów):
GC buduje graf dostępnych obiektów, przechodząc rekurencyjnie przez wszystkie referencje. Obiekty nieosiągalne (czyli takie, do których nie prowadzi żadna ścieżka od korzenia) są oznaczane jako do usunięcia.

    \item Collection (czyszczenie):
W zależności od poziomu presji pamięci:

Najpierw oczyszczana jest generacja 0.

Jeśli to nie wystarczy – oczyszczana jest generacja 1.

W ostateczności – generacja 2 (tzw. full GC).
Obiekty, które przetrwały kolekcję, są promowane do starszej generacji (np. z Gen0 → Gen1).

    \item Compaction (kompresja):
Po zwolnieniu pamięci GC kompaktuje stertę, przesuwając żywe obiekty tak, aby tworzyły ciągły blok pamięci. Dzięki temu unika się fragmentacji i utrzymuje szybkie tempo alokacji (alokator może dalej inkrementować wskaźnik liniowo).
Kompresja może być pominięta w przypadku dużych stert lub LOH.
\end{enumerate}
}

\work{GC Modes}{
GC w .NET występuje w kilku wariantach konfigurowanych podczas uruchomienia aplikacji:

\begin{center}
\begin{tabular}{lp{11cm}}
Tryb & Charakterystyka \\
Workstation GC & Domyślny dla aplikacji desktopowych. Mniejsze opóźnienia, mniejsze zużycie CPU. \\
Server GC & Dla aplikacji serwerowych i wielowątkowych. Tworzy osobną stertę i wątek GC dla każdego rdzenia CPU, zapewnia wysoką przepustowość. \\
Background GC & Umożliwia czyszczenie generacji 2 w tle, podczas gdy generacje 0 i 1 mogą być odśmiecane w tym samym czasie — minimalizuje pauzy. \\
Low Latency / Sustained Low Latency & Ogranicza lub czasowo wyłącza czyszczenie generacji 2, stosowany w scenariuszach czasu rzeczywistego (np. rendering, UI).\\
\end{tabular}
\end{center}

Garbage Collector w .NET jest niedeterministyczny, co oznacza, że moment jego uruchomienia nie jest z góry znany i nie można go w pełni przewidzieć.
Istnieje metoda GC.Collect(), która sugeruje wykonanie kolekcji, ale jej użycie jest zalecane tylko w wyjątkowych sytuacjach diagnostycznych.

Obiekty mogą implementować destruktor (finalizer), który pozwala zwolnić niezarządzane zasoby (np. uchwyty do plików lub połączenia). Jednak finalizacja odbywa się w osobnym wątku i może opóźniać odzyskanie pamięci. Z tego powodu w .NET zaleca się stosowanie wzorca IDisposable i bloku using, które pozwalają deterministycznie zarządzać niezarządzanymi zasobami.

\begin{tabular}{lp{11cm}}
Cecha & Opis \\
Typ GC & Tracing, generacyjny, kompaktujący \\
Liczba generacji & 3 (0, 1, 2) + LOH \\
Strategia alokacji & Wskaźnik liniowy (bump pointer) \\
Promocja obiektów & Po przeżyciu kolekcji \\
Kompaktowanie & Domyślnie włączone (oprócz LOH) \\
Tryby pracy & Workstation, Server, Background, Low Latency
Deterministyczność & Niedeterministyczny \\
Zarządzanie niezarządzanymi zasobami & Poprzez IDisposable lub finalizery \\
\end{tabular}

}

\work{Component Object Model}{

Microsoft's Component Object Model (COM) and WinRT makes pervasive use of reference counting. In fact, two of the three methods that all COM objects must provide (in the IUnknown interface) increment or decrement the reference count. Much of the Windows Shell and many Windows applications (including MS Internet Explorer, MS Office, and countless third-party products) are built on COM, demonstrating the viability of reference counting in large-scale systems.
}

\header{Functional Programming}

\work{Monoid}

\end{document}
";
