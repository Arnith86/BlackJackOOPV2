# Factory and Product Documentation Checklist

---

## ✅ **Factory Creator Summary Checklist**

**Use when writing summaries for classes like `CardCreator`, `PlayerCreator`, etc.**

```csharp
/// <summary>
///     [1] Concrete/abstract creator for the [ProductName] factory pattern.
///     [2] Responsible for creating fully initialized <see cref="[ProductType]"/> objects.
///     [3] Part of the [SystemName] construction pipeline (if relevant).
///     [4] Typically used when a [GameContext/Scenario] needs a new [Thing].
/// </summary>
```

### ✍ Example:
```csharp
/// <summary>
///     Concrete creator for the Card factory pattern.
///     Responsible for creating fully initialized <see cref="BlackJackCard"/> objects.
///     Used when generating new playing cards in a BlackJack round.
/// </summary>
```

---

## ✅ **Product Interface/Class Summary Checklist**

**Use for interfaces like `ICard`, `IBlackJackCardHand`, etc.**

```csharp
/// <summary>
///     [1] Represents a [What the class is] used in the [Game/System].
///     [2] Contains properties for [Core Properties].
///     [3] Includes behavior for [Key Methods or State Changes].
///     [4] Designed to be flexible via generic types <typeparamref name="TImage"/> and <typeparamref name="TValue"/>.
/// </summary>
```

### ✍ Example:
```csharp
/// <summary>
///     Represents a playing card in the BlackJack game.
///     Contains images for front and back, a value, and a flag indicating if it’s face down.
///     Includes functionality to flip the card.
///     Designed to support flexible image and value types via generics.
/// </summary>
```

---

### ✅ Tips for Both

| Do                             | Avoid                          |
|-------------------------------|---------------------------------|
| Use `<see cref="Type"/>`      | Listing every method in the summary |
| Keep it under 4 sentences      | Repeating what the code already says |
| Use consistent terminology     | Mixing metaphors or vague terms |
| Mention generics when used     | Skipping key behaviors (like Flip) |

---

Would you like these as a snippet file or markdown doc you can drop into your solution for others to follow too?