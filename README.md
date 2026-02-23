# UP-Events

Type-safe EventBus system for Unity.

UP-Events cung cấp một hệ thống EventBus nhẹ, type-safe (TEvents), hỗ trợ quản lý vòng đời subscription thông qua IDisposable, giúp giảm coupling giữa các hệ thống trong game.

---

## ✨ Features

- Generic type-safe EventBus (`Publish<T>`, `Subscribe<T>`)
- Subscription trả về `IDisposable`
- `CompositeDisposable` để gom nhiều subscription
- Unity lifetime helpers:
    - `DisposeOnDestroy`
    - `DisposeOnDisable`
    - Extension `AddTo(...)`
- Không phụ thuộc Editor
- Không thread-safe (thiết kế cho Unity main thread)

---

## 📦 Package Structure

Runtime:

```
HTDA.Framework.Events
├── IEventBus
├── EventBus
├── Subscription
├── CompositeDisposable
└── Unity/
├── DisposeOnDestroy
├── DisposeOnDisable
└── DisposableExtensions
```

Samples:

```
Samples~/EventBusDemo
```

---

## 🚀 Quick Start

### 1️⃣ Tạo EventBus

```csharp
using HTDA.Framework.Events;

IEventBus bus = new EventBus();
```

### 2️⃣ Định nghĩa TEvent
```csharp
public readonly struct PlayerDied
{
    public readonly int PlayerId;
    public PlayerDied(int id) => PlayerId = id;
}
```
### 3️⃣ Subscribe
```csharp
bus.Subscribe<PlayerDied>(e =>
{
    Debug.Log($"Player died: {e.PlayerId}");
});
```

### 4️⃣ Publish
```csharp
bus.Publish(new PlayerDied(1));
```

### 🔄 Auto Unsubscribe (Recommended)

Để tránh memory leak trong Unity:
```csharp
using HTDA.Framework.Events.Unity;

var disposer = this.GetOrAddDisposeOnDestroy();

bus.Subscribe<PlayerDied>(OnPlayerDied)
   .AddTo(disposer);
```

Khi GameObject bị Destroy → subscription tự động Dispose.

---

## 🧱 CompositeDisposable
```csharp
var bag = new CompositeDisposable();

bus.Subscribe<A>(OnA).AddTo(bag);
bus.Subscribe<B>(OnB).AddTo(bag);

bag.Dispose(); // Unsubscribe tất cả
```

⚠ Design Notes

- Không thread-safe (Unity main thread only)

- Event dispatch là synchronous

- Không giữ reference weak — cần dispose đúng cách

---

## 🎯 Intended Usage

UP-Events được dùng cho:

- Gameplay events (Damage, Death, Score)

- UI communication

- Scene flow signals

- Analytics hooks

- Decoupled system messaging

---
## 📌 Dependency

Depends on:

- UP-Core

Không phụ thuộc:

- FSM

- Pooling

- SceneFlow
---
## 📄 License
[LICENSE.md](LICENSE.md)