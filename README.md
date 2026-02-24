# UP-Events (HTDA.Framework.Events)

EventBus nhẹ, type-safe, phù hợp để giảm coupling giữa các system/module.

## Features

- `IEventBus` / `EventBus`
- Subscribe/Unsubscribe bằng `IDisposable`
- `CompositeDisposable`
- Helper cho Unity: `DisposeOnDestroy`, `DisposeOnDisable`

## Quick start

```csharp
IEventBus bus = new EventBus();

var sub = bus.Subscribe<PlayerDied>(e => { /* ... */ });
bus.Publish(new PlayerDied());

// cleanup
sub.Dispose();
```

## Unity helpers

```csharp
bus.Subscribe<MyEvent>(OnEvent)
   .DisposeOnDestroy(this);
```

## Sample

Package có sample **EventBusDemo** (import từ Package Manager → Samples).
