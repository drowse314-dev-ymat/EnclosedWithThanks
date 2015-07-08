## Enclosed With Thanks :)

`EnclosedWithThanks` provides tiny `IDisposable` patterns for hasty .NET coders.


### Usage

With `Action`:

```csharp
var tempFilePath = "temp.txt";

using (
    Enclosing.GetEnclosure(
        onOpen: () => { File.WriteAllText(tempFilePath, "tempfile content"); },
        onClose: () => { File.Delete(tempFilePath); }))
{
    File.ReadAllText(tempFilePath);
}
```

With your `Enclosing` subclass with default constructor:

```csharp
class Counter : Enclosing
{
    private int count = 0;
    public int Count { get; set; }
}
```
Use the instance inside your `Action` or `using` clause: 

```csharp
using (
    var counter = Enclosing.GetEnclosure<Counter>(
        onOpen: (counter) => { counter.Count++; },
        onClose: (counter) => { counter.Count++; }))
{
    conter.Count--;
}
```


### License

MIT License.
