# Tiger Run Clicker

🇬🇧 [Read in English](README.md)

Idle-кликер на Unity. Управляй тигриной экономикой — строй магазины, зарабатывай ресурсы и расширяй флот.

---

## Демо

https://github.com/nnivis/TigerRunClicker/assets/105976276/7ff27889-e294-4fe7-adc3-6a7413da946d

---

## О проекте

Tiger Run Clicker — портфолио-проект с акцентом на чистую архитектуру в Unity. Игрок строит Мясные лавки и Банки, которые пассивно генерируют Монеты и Мясо, затем тратит ресурсы на покупку новых тигров и зданий. Прогресс сохраняется локально между сессиями.

---

## Механики

- Пассивная генерация ресурсов от зданий (Мясные лавки → Монеты, Банки → Мясо)
- Покупка дополнительных тигров и зданий с динамическим ценообразованием
- Механика ускорения для активной игры
- Система сохранений — прогресс восстанавливается при перезапуске

---

## Архитектура

| Паттерн | Реализация |
|---|---|
| Dependency Injection | VContainer — `RootLifetimeScope` связывает все сервисы |
| State Machine | `StateMachineBehavior` — состояния Menu, Game, Settings, Record |
| Presenter | `GamePresenter` отделяет UI от игровой логики |
| Сервисный слой | `PurchaseService`, `GameEntityManager` — единая ответственность |
| Слой данных | `IPersistentData` / `IDataProvider` — JSON через Newtonsoft.Json |

---

## Технологии

- **Движок:** Unity 6000.3 LTS
- **Язык:** C#
- **DI:** VContainer 1.17.0
- **Анимация:** DOTween
- **UI:** TextMeshPro
- **Сериализация:** Newtonsoft.Json 3.2.2

---

## Запуск

1. Клонировать репозиторий
2. Открыть в Unity 6000.3 или новее
3. Открыть `Assets/Scenes/GameScene`
4. Установить VContainer через Package Manager: `jp.hadashikick.vcontainer`
5. Нажать Play

---

## Контакты

- GitHub: [nnivis](https://github.com/nnivis)
