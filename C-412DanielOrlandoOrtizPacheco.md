# Diferencia entre int e Interger
La principal diferencia entre int e Integer en Java es que int es un tipo primitivo del lenguaje mientras Integer
es una clase la cual contiene un valor int en su definición.
Las variables int son mutables, es decir que estas pueden cambiar su valor a lo largo de la ejecución a diferencia 
de una variable Integer estas solo pueden cambiar su valor asignandole un nuevo Integer.
Las variables int solo almacenan el valor real del entero no tienen metodos en su definicion, a diferencia de Integer
que es la clase que contiene los metodos definidos sobre int
La diferencia con C# es que en C# bool es un alias para System.Boolean al igual que int para System.Int32.

# Method implementado en el cuerpo de las Interface C# 8.0

Los miembros implementados por defecto en interfaces es una característica agregada desde C#8, 
esta nueva característica hace posible que el creador de una librería pueda añadir métodos, 
propiedades, etc. a una interfaz en versiones futuras
sin romper la compatibilidad a nivel binario o de código fuente con las implementaciones
de la interfaz que ya pudieran existir. Otros lenguajes modernos, como Java y Swift,
ofrecen desde hace algún tiempo características similares.

Las implementaciones por defecto sólo serán accesibles a través de las interfaces que las definen,
y no de las clases que las usan. 
por ejemplo:

```csharp
public interface IAnimal
{
    void Info() => Console.WriteLine("I'm an animal");
}

public class Cat : IAnimal
{
    public string Name { get; }

    public Cat(string name)
    {
        Name = name;
    }
}

class Program
{
    static void Main(string[] args)
    {
        Cat garfield = new Cat("Garfield");
        IAnimal felix = new Cat("Felix");

        garfield.Info(); // Compilation error: Info() is not a member of Cat
        felix.Info();    // Ok: Info() is a member of IAnimal --> "I'm an animal"
    }
}
```

Sin embargo, si la clase implementa su propia versión del método definido en la interfaz, será éste el 
utilizado en tiempo de ejecución, por ejemplo:

```csharp
public interface IAnimal
{
    void Info() => Console.WriteLine("I'm an animal");
}

public class Cat : IAnimal
{
    public string Name { get; }

    public Cat(string name)
    {
        Name = name;
    }

    public void Info() => Console.WriteLine($"I'm {Name} the cat");
}

class Program
{
    static void Main(string[] args)
    {
        Cat garfield = new Cat("Garfield");
        IAnimal felix = new Cat("Felix");

        garfield.Info(); // Ok: Info() is a member of Cat --> "I'm Gardfield the cat"
        felix.Info();    // Ok: Info() is a member of IAnimal --> "I'm Felix the cat"
    }
}
```

# Swift array covariante

Swift implementa los arrays con el comportamiento de copy on write.
En un nivel básico, es solo una estructura que contiene una referencia a un búfer asignado por 
montón que contiene los elementos, por lo que varias instancias pueden hacer referencia al mismo 
búfer. Cuando venga a mutar una instancia de matriz determinada, la implementación comprobará 
si se hace referencia al búfer de forma única y, si es así, mutarlo directamente. 
De lo contrario, la matriz realizará una copia del búfer subyacente para conservar la 
semántica de valores

Sin embargo, con la estructura: no está implementando copia en escritura a nivel de idioma. 
Por supuesto, esto no impide que el compilador realice todo tipo de optimizaciones para minimizar 
el costo de copiar estructuras enteras. Sin embargo, estas optimizaciones no tienen por qué seguir
el comportamiento exacto de copiar en escritura: el compilador es simplemente libre de hacer lo que
desee, siempre y cuando el programa se ejecute de acuerdo con la especificación del idioma

Y si fueran variables locales en una función, por ejemplo:

```swift
struct Point {
    var x: Float = 0
}

func foo() {
    var p1 = Point()
    var p2 = p1
    p2.x += 1
    print(p2.x)
}

foo()
```

El compilador ni siquiera tiene que crear dos instancias para empezar: solo tiene que crear una 
única variable local de punto flotante inicializada e imprimirla (1.0)

Con respecto a la aprobación de tipos de valor como argumentos de función, para tipos lo suficientemente
grandes y (en el caso de estructuras) funciones que utilizan suficiente de sus propiedades, el compilador
puede pasarlos por referencia en lugar de copiar. A continuación, el destinatario solo puede hacer una
copia de ellos si es necesario, por ejemplo, cuando es necesario trabajar con una copia mutable.

En otros casos en los que las estructuras se pasan por valor, también es posible que el compilador
especialice funciones para copiar solo entre las propiedades que necesita la función.
```swift
struct Point {
    var x: Float = 0
    var y: Float = 1
}

func foo(p: Point) {
    print(p.x)
}

var p1 = Point()
foo(p: p1)
```
Suponiendo que el compilador no es lineal (lo hará en este ejemplo, pero una vez que su implementación 
alcance un cierto tamaño, el compilador no pensará que vale la pena). 
El compilador puede especializar la función como:

```swift
func foo(px: Float) {
    print(px)
}

foo(px: 0)
```
Sólo pasa el valor de la propiedad en la función, ahorrando así el costo de copiar la estructura

Por lo tanto, el compilador hará todo lo posible para reducir la copia de tipos de valor. Pero con 
tantas optimizaciones en diferentes circunstancias, no se puede simplemente hervir el comportamiento 
optimizado de los tipos de valor arbitrario a sólo copiar en escritura.