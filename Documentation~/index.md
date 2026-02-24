# UP-Events – Documentation

## 1. Design goals

- Type-safe, dễ dùng.
- Ít allocations, không LINQ.
- Subscription trả về `IDisposable` để quản lý vòng đời rõ ràng.

## 2. API

### Publish
- `Publish<T>(T evt)`

### Subscribe
- `Subscribe<T>(Action<T> handler) -> IDisposable`

### CompositeDisposable
- Gom nhiều disposable để dispose một lần.

## 3. Unity integration

- `DisposeOnDestroy`: tự dispose khi GameObject bị destroy.
- `DisposeOnDisable`: tự dispose khi bị disable.

Khuyến nghị:
- Luôn dispose subscription để tránh leak/handler chạy sai thời điểm.
- Đặt event struct nhỏ, immutable (readonly struct) để tránh GC.

## 4. Khi nào nên dùng events vs direct call

- Dùng EventBus khi cần loose coupling (UI ↔ gameplay, analytics, achievements…)
- Tránh bắn event cho flow cực nóng nếu không cần (micro-optimizations); dùng direct call hoặc callback.
