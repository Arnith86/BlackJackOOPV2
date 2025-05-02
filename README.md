# BlackJackV2

**BlackJackV2** is a fully restructured version of a Blackjack game originally developed as part of a final assignment in object-oriented programming. While the first version showed early efforts, this second iteration is a comprehensive rewrite focused on clean architecture, maintainability, and scalability.

Key improvements in this version include:
- Clear separation of concerns using the **MVVM** pattern.
- Decoupled and testable architecture via **Dependency Injection**.
- Modular design through **interface-driven factories** and **event-based communication**.
- Enhanced gameplay features, such as **multi-player support** and the ability to **split hands**.
- Well-structured and reusable components using **generic abstractions** (`ICard<TImage, TValue>`, `ICardDeck<TImage, TValue>`, etc.).
- Organized folder structure aligned with scalable project practices (e.g., `/Models`, `/Services`, `/Factories`, `/Shared`, `/ViewModels`, `/Views`).

The goal of this project is to serve both as a feature-rich game and a learning base for how to structure large-scale Avalonia UI applications using best practices.

## 🛠 Tech Stack

**Frameworks & Libraries:**
- **Avalonia UI** – Cross-platform .NET UI framework for building rich desktop applications.
- **ReactiveUI** – MVVM framework for reactive programming and state management.
- **Microsoft.Extensions.DependencyInjection** – Built-in dependency injection framework for .NET.

**Languages & Runtime:**
- **C#** – Main programming language.
- **.NET 8** – Runtime for cross-platform development.

**Architecture & Patterns:**
- **MVVM (Model-View-ViewModel)** – Core UI architectural pattern.
- **SOLID principles** – Clean, modular design.
- **Dependency Injection** – Decoupled service and object creation.
- **Factory Pattern** – Centralized, testable object creation.
- **Event-Driven Design** – For handling player actions and communication between components.
- **Generics & Interfaces** – Promoting reusability across game components.

**Project Structure Highlights:**
- `/Models` – Game logic and data structures.
- `/ViewModels` – State and logic bound to the UI.
- `/Views` – Avalonia XAML views.
- `/Factories` – ViewModel and model creation logic.
- `/Services` – Event handlers, dependency wiring, and game service orchestration.
- `/Shared` – Constants and utility classes.
