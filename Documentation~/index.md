# UP-Events – Technical Overview

## 1. Architecture

EventBus sử dụng Dictionary<Type, Delegate> để map event type với callback.

Key design:

- Mỗi TEvent là một type riêng
- Delegate chain được combine thông qua +=
- Subscription được quản lý qua IDisposable

---

## 2. Core API

### IEventBus

```csharp
IDisposable Subscribe<T>(Action<T> handler);
void Unsubscribe<T>(Action<T> handler);
void Publish<T>(T evt);
void Clear();
```

---
## 3. Subscription Lifecycle

Subscribe trả về:
```csharp
IDisposable
```
Khi Dispose:

- Handler được remove khỏi delegate chain

- Safe call nhiều lần (idempotent)

---

## 4. Memory Management Strategy

Unity không có GC event weak-reference mặc định.

UP-Events giải quyết bằng:

- IDisposable subscription

- CompositeDisposable

- DisposeOnDestroy

- DisposeOnDisable

=> Developer phải chủ động quản lý lifecycle.

---
## 5. Best Practices

✔ Dùng struct cho TEvent nếu nhỏ và immutable
✔ Không giữ state trong EventBus
✔ Không publish trong constructor
✔ Dispose trong OnDestroy / OnDisable

---

## 6. Limitations

- Không thread-safe

- Không hỗ trợ sticky events

- Không có event priority

- Không queue async

Nếu cần advanced features → mở rộng trong version sau.
---
## 7. Future Extensions (Optional)

- StickyEventBus

- AsyncEventBus

- Event profiling tools

- Event logging toggle (Editor only)
---
## 8. Versioning Strategy

v1.0.0:

- Core EventBus

- IDisposable subscriptions

- CompositeDisposable

- Unity lifecycle helpers

- Sample demo
---
## 9. Integration With Other Packages

UP-FSM:

- Có thể publish StateChangedEvent

UP-SceneFlow:

- Publish SceneLoadedEvent

UP-Pooling:

- Publish SpawnedEvent / DespawnedEvent

UP-Core:

- Singleton / ServiceRegistry có thể giữ EventBus instance