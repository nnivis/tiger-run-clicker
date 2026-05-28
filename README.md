# Tiger Run Clicker

🇷🇺 [Читать на русском](README.ru.md)

An idle clicker game built in Unity. Manage a tiger-powered city economy — build shops, earn resources, and expand your fleet.

---

## Demo

https://github.com/nnivis/TigerRunClicker/assets/105976276/7ff27889-e294-4fe7-adc3-6a7413da946d

---

## About

Tiger Run Clicker is a portfolio project focused on clean architecture in Unity. Players build Butcheries and Banks that passively generate Coins and Meat, then spend those resources to purchase more tigers and buildings. Progress is saved locally between sessions.

---

## Features

- Idle resource generation from buildings (Butcheries → Coins, Banks → Meat)
- Purchase additional tigers and buildings with dynamic pricing
- Speed boost mechanic for active play
- Persistent save system — progress restores on restart

---

## Architecture

| Pattern | Implementation |
|---|---|
| Dependency Injection | VContainer — `RootLifetimeScope` wires all services |
| State Machine | `StateMachineBehavior` — Menu, Game, Settings, Record states |
| Presenter | `GamePresenter` decouples UI from game logic |
| Service layer | `PurchaseService`, `GameEntityManager` — single responsibility |
| Data layer | `IPersistentData` / `IDataProvider` — JSON via Newtonsoft.Json |

---

## Tech Stack

- **Engine:** Unity 6000.3 LTS
- **Language:** C#
- **DI:** VContainer 1.17.0
- **Animation:** DOTween
- **UI:** TextMeshPro
- **Serialization:** Newtonsoft.Json 3.2.2

---

## Getting Started

1. Clone the repository
2. Open in Unity 6000.3 or newer
3. Open `Assets/Scenes/GameScene`
4. Install VContainer via Package Manager: `jp.hadashikick.vcontainer`
5. Press Play

---

## Contact

- GitHub: [nnivis](https://github.com/nnivis)
