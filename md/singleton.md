# Patrón de diseño

Los patrones de diseño son unas técnicas para resolver problemas comunes en el desarrollo de software, 
problemas que generalmente estan fuertemente realcionados con el diseño de la aplicación.Estos se 
caracterizan por ser en su gran mayoria altamente reusables y los conflictos que los originaros
aparecen con gran frecuencias y con formas muy diversas el mundo del desarrollo de software.

Los patrones de diseño fueron introducidos por primera vez por GoF(Gang of Four) donde describían 
los patrones comunes como problemas que ocurren una y otra vez y soluciones a esos problemas.
Los patrones de diseño tienen cuatro elementos esenciales: 
- **Nombre** : es un identificador que podemos usar para describir un problema de diseño, sus soluciones y 
consecuencias en una o dos palabras. 
- **Problema** : describe cuándo aplicar el patrón. 
- **Solucion** : describe los elementos que conforman el diseño, sus relaciones, responsabilidades y colaboraciones. 
- **Consecuencias** : son los resultados y las compensaciones de aplicar el patrón.  

Los patrones de diseño pretenden: 
- Proporcionar catálogos de elementos reusables en el diseño de sistemas software.
- Evitar la reiteración en la búsqueda de soluciones a problemas ya conocidos y solucionados anteriormente.
- Formalizar un vocabulario común entre diseñadores.
- Estandarizar el modo en que se realiza el diseño.
- Facilitar el aprendizaje de las nuevas generaciones de diseñadores condensando conocimiento ya existente.

Los patrones de diseño se pueden separar según sus caracteristicas y problemas que resuelven en varias categorias
por ejemplo: _Patrón Creacional_, _Patrón Estructural_ , _Patrón de Comportamiento_. Los **Patrón Creacional** 
son aquellos que se ocupan de cómo se puede crear el objeto y aíslan los detalles de la creación del objeto. 
Los **Patrón Estructural** son aquellos que diseñan la estructura de clases y objetos para que puedan 
componerse para lograr resultados más grandes. Los **Patrón de Comportamiento** son aquellos que se ocupan de la 
interacción entre los objetos y la responsabilidad de los objetos. 

# Patrón Singleton

> El patrón de diseño __Singleton__ (instancia única) está pensado para restringir la creación de objetos 
pertenecientes a un tipo determinado a un único objeto. Su intención consiste en garantizar que dicho tipo 
sólo tenga una instancia y proporcionar un punto de acceso global a ella. 

Este patrón es un **Patrón creacional**. El mismo se suele utilizar 
cuando una clase controla el acceso a un recurso físico único o cuando hay datos que deben estar disponibles 
para todos los objetos de la aplicación, pero en general, la idea del **Singleton** es extensible a disimiles 
escenarios. En su versión mas general la implementación de una clase que siga el patrón de diseño **Singleton**
consta de al ausencia de constructores públicos y metodos estáticos; los cuales revisan si el objeto ha sido 
instanciado con anterioridad en caso negativo estos llaman a algun constructor de la clase en cuestión y 
guarda el objeto creado en una variable estática, en caso contrario (el objeto ya fue instanciado anteriormente) 
retornan la referencia al objeto creado anteriormente.

```csharp
public class HeroSingleton
{
    private static HeroSingleton instance;
    private HeroSingleton() {}
    public static HeroSingleton GetInstance => instance == null ? instance = new HeroSingleton() : instance;
    /*Implementacion del concepto Hero*/
    ....
}
```

Notese que en el ejemplo anterior es claro que se están mezclando dos responsabilidades diferentes en la misma clase.
Lo cual atenta contra el patrón **Single-Responsibility** uno de los patrónes de diseño más importantes; el cual 
es conocido como uno de los 5 principios básicos del paradigma **OOD**, los cuales se resumen 
con las siglas **SOLID**. Segun **Single-Responsibility** una clase sólo debería preocuparse por sus lógica de negocio.
Y no de que la aplicación general necesita un control **Singleton** sobre las instancias de dicha clase 
Pero también es fácil ver la siguiente solución a este conflicto:

```csharp
public static class HeroSingleton
{
    private static Hero instance;
    public static Hero GetInstance => instance == null ? instance = new Hero() : instance;
}
public class Hero
{
    private Hero() {} // para ser concecuente con el patrón
    /*Implementacion del concepto Hero*/
    ....
}
```
Notese además que en los ultimos dos ejemplos existe un fuerte dependencia entre clases; provocando asi que la clase 
**HeroSingleton**, que contiene la implemetación del patrón **Singleton**, solo tiene sentido bajo la existencia de la 
clase **Hero**. Por tanto, esta implementacion del patron **Singleton** no es ni extensible y reusable.
Para mejorar nuestra implementacion podemos apoyarnos en la __D__ de **SOLID** (**Dependency Inversion**) y crear una
clase **Singleton** que sepa gestionar el patrón y contenga mecanismos para obtener las dependencias ( tipo del objeto 
al que se decea controlar ):  

```csharp
public class Singleton<T> where T : class
{
    private static T instance;
    public static T GetInstance => instance == null ? instance = (T)Activator.CreateInstance(typeof(T), true) : instance;
}
```

Esta implementación es bastante general a la par que sencilla, tan sencilla que `Activator.CreateInstance(typeof(T), true)` 
solo funciona para las tipos `T` que tiene un constructor que no requiera de parámetros. Con una implementación de 
este estilo se logra una clase que controla la lógica del patrón, menos la característica del constructor privado, independiente de a que clase se le vaya a aplicar el patrón 

Aunque con la clase `Singleton<T>` se pude utilizar el patrón con la sintaxis: 

```csharp
    ...
    Hero hero = Hero.GetInstance;
    ...
```

Mediante la herencia:

```csharp
class Hero : Singleton<Hero> { } 
```
Sin embargo los autores prefieren utilizarla como en el ejemplo siguiente pues esta es más expresiva y además se 
ajusta mejor a la idea del patrón **Single-Responsibility**  

```csharp
 dynamic objA = Singleton<ExpandoObject>.GetInstance;
 dynamic objB = Singleton<ExpandoObject>.GetInstance;
 objA.name = "hola";
 Console.WriteLine(objB.name);// hola

```
>Vease **JORGITO PON LA DIRECCION** 
>para una implementación mas robusta del método `GetInstance`  
<!--###########################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################################-->

# ¿ Que pasa con __Python__?

Tras ver las dos bases principales de le patrón **Singleton** (los constructores privados y los métodos estáticos) es común cuestionarse si en un lenguaje como __Python__; donde no existe la posibilidad de ocurtar ningún atributo, pues no contiene conceptos como **public** y **private** para controlar la visibilidad de los atributos de una clase, no es posible lograr una implementación concistente de este patrón. La respuesta es **si**; y de hecho se puede lograr una real separación de conceptos y responsabilidades, no como en __C#__ que la consistencia del patrón depende de que la clase a la que se le aplicará el patrón marque sus constructores con **private**. La llave para la implentación del patrón en __Python__ esta en la definición de lo que es una clase en __Python__ y el mecanismo del que se vale el lenguaje para generar instancias de estas clases, contenidos que se abordarán a continuación

# Naturaleza de las clases en __Python__

__Python__ es un lenguaje en el que predomina el paradigma orientado a objeto. En el mismo todo se maneja a nive de objetos incluido las propias clases. Cada uno tipos **buil-in** y los tipos personalizados (clases) definidos con la sintaxis **class** tienen un a metaclase propia de las cuales estos tipos son instancias. Las metaclases son clases que sus instancias son tipos (clases), tipos que a su vez sus instancias son los objetos con los cuales todos los programador del paradidma **OOD** esta familiarizado. Aunque se __Python__ permite crear metaclases personalizadas no es una práctica muy común, pues muchos de los posibles problemas que se podrían modelarse con ellas también se pueden modelar con otras herramientas del lenguaje, aun así 
tener conocimiento sobre las mismas aportaría un gran nivel de compresión sobre el funcionamiento interno del lenguaje. La gran
mayoría de los tipos de __Python__, tanto integrados como personalizados, tiene como metaclase propia una instancia de la metaclase básica de lenguaje **type**

## Metaclase

La definición literal es: "_Una metaclase es una clase cuya instancias son clases_". Por lo que se puede resumir que las 
metaclase son las clases en donde esta escrito el código que describe el comportamiento de sus clases instancias, al igual 
que una clase "ordinaria" define el comportamiento de sus objetos instancias, lo cual es exactamente lo que sucede en __Python__. Notese además que según la definición una metaclase es una clase, por lo que ella también debe tener una metaclase que la manege este proceso que parece infinito no es tal en __Python__. El interprete del lenguaje antes de iniciar el análisis del código crea una instancia de la metaclase básica **type** para que esta se encarge de instanciar a todas las metaclases venideras.      
Las metaclases se esconde detrás de prácticamente todo el código __Python__ las está utilizando independientemente de si lo sabe o no, aunque como se mención anteriormente su uso explísito no es muy común.

Las metaclases no son compatibles con todos los lenguajes de programación orientados a objetos. Esos lenguajes de programación, que admiten metaclases, varían considerablemente en la forma en que los implementan. Algunos programadores ven las metaclases en
__Python__ como "soluciones esperando un problema",pues aunque son númerosos su posibles uso también existen otras herraminentas para dichos usos. A continuación se nombran algunos de los problemas que se puden resolver con las metaclases:

- Comprobación de interfaz
- Registrar clases en el momento de la creación
- Agregar automáticamente nuevos métodos
- Creación automática de propiedades
- Bloqueo / sincronización automática de recursos.

__Python__ ofrece la posiblidad de definir metaclases personalizadas y asignar una metaclase dada a una clase específica.
La única diferencia entre la definición sintáctica de un clase y la de una metaclase es que esta última debe heredar explisitamente de **type** y algunos métodos como `__init__`, `__call__ ` y `__new__` requieren de algunos parámetros específicos, el significado de estos parámetros se detalla más tarde. Y para seleccionar una metaclase específica para una clase se debe hacer uso de la palabra reselvada **metaclass** a la cual se le debe asignar el nombre de la metaclase en cuestión ( en realidad al asignar el nombre se esta asignando la metaclase de la metaclase en cuestión, lo que se explica mas adelante mas adelante ) entre las distintas herencias. A continuación un ejemplo de lo antes expuesto:

```python
class Meta(type):

    def __init__(self, name, _list, _dir):
        self.metaclass = "Meta"

    def print_class_metaclass(self):
        return f'class:{self.__name__} and metaclass: Meta'

    def __str__(self):
        return self.print_class_metaclass()
    
    def __add__(self,other):
        return f'class: {self.__name__} + class: {other.__name__} '

class MyClass(metaclass = Meta):
    pass

```

Notese que la sintácsis de definición de las metaclases efectivamente es extremadamente similar a la de las clases "ordinarias", lo cual es coherente con la definión pues las mismas tambien son clases. La sintaxis para el control de las metaclase de una clase espesífica e el nombre de dicha clase ( razón por la cual se comentaba que a la palabra reservada **metaclass** se le asigna la metaclase de la metaclase que controlara la clase en cuestión ), el nombre de dicha clase es la puerta para acceder la los métodos y propiedades de la metaclase que la controla. Se puede utlizar la implemetación anterior para ejemplificar esta sintaxis:

```python

print(f'class:{MyClass.__name__} and metaclass: {MyClass.metaclass} ')  #class:MyClass and metaclass: Meta
print(MyClass.print_class_metaclass())                                  #class:MyClass and metaclass: Meta
print(MyClass)                                                          #class:MyClass and metaclass: Meta
print(MyClass + MyClass)                                                #class: MyClass + class: MyClass

```

Notese con que con las metaclases se puede simular un comportamiento estático de la clase, pero la comunidad pythoniana logicamente prefiere utilizar los decoradores `@staticmethod` y `@property` para lograr este efecto. Pero con ya se ah comentado las utilidades son muy diversas, como en el ejemplo anterior se puede lograr una sintaxis que simule de operaciones aritmeticas entre clases.

## ¿ Cuando se instancian las metaclases ? ¿ Como __Python__ instancia una clase ? 

En Python todo es un objeto incluido las clases. Las declaraciones de clases mediante la palabra reservada **class** son una 
de las primeras cosas que se analizan en el proceso de interpretación y ejecución de un programa en __Python__. Dicho análisis   
se resume en construir una "terna" que contenga toda la información necesatia sobre dicha clase.
Esta "terna" esta compuesta por: nombre de la clase, una lista con las distintas clases del las que dicha clase hereda y un diccionario con la información de los atributos y metodos de la futura clase.
Primero utilizando la lista de clases heredadas se realiza una busqueda de la metaclass que controlara el funcionamineto de esta clase. Indistintamente si se encuentra una metaclase personalizada o es simplemente se alcanza la metaclase principal del lenguaje (**type**) de dicha metaclase se crea una nueva instacia mediante el mismo mecanismo que se explica más adelante, sus método `__new__` y `__init__` los cuales deben resibir como párametros la información de la clase que esta concentrada en la "trena" más la palabra reservada **cls** referencia a la clase o instancia de la metaclase en cuestión y su propia metaclase (**type**) para que controle el funcionamineto de la clase en cuetión. También es importante aclarar que sin importar que la metaclase que controle una clase sea personalizada la lógica necesaria para crear instancias de metaclases se encuentran en la clase **type**, por lo que una metaclase personalizada que quiera añadir comportaminetos a una instancia de una metaclase deberá segui el patrón:

```python
class Meta(type):
    def __init__(cls, name, _list, _dir):
        print("Meta.__init__() --> Inicializando la metaclase")
    def __new__(cls, name, _list, _dir):
        # antes de crear la instancia de la metaclase 
        x = super().__new__(cls,name, _list, _dir) # llamando al metodo __new__ de type
        # despues de crear la instacian de la metaclase
        print("Meta.__new__() --> Creando una isntancia de la metaclase que controlará a la clase")
        return x #retornar la instancia de la metaclase 
```

El paso siguiente, luego de saber que sucede con las declaración del cuerpo de la clase, es profundizar en el mecanismo que hay detrás la instanciación de la clase en cuestión. Cuando el intérprete se encuentra con `MyClass()` realiza una llamada al metodo `__call__` del la respectiva metaclase, de igual manera la lógica necesaria para crear instancias de clases se encuentran en la clase **type**, por lo que una metaclase personalizada que quiera añadir comportaminetos a una instancia de una clase deberá segui el patrón   

```python
class Meta(type):
    def __call__(cls):
        print("Meta.__call__() --> Creando una isntancia de la clase que controlará el objeto")
        # antes de crear la instancia de la clase 
        x = super(Meta,cls).__call__() # llamando al metodo __call__ de type base de cls instancia especifica de la metaclase Meta
        # despues de crear la instacian de la clase
        print("Meta.__call__() --> Instancia de la clase que controlará al objeto ya creada")
        return x #retornar la instancia de la clase 
```   

Dicha lógica para crear instancias de una clase, que se encuentra implementada en la metaclase **type**, se apoya en los métodos 
`__new__` y `__init__` de la clase que esta siendo procesada. El método `__new__` crea y returna una nueva instancia clase en cuestión apoyandose en la implementación de este mismo método de la clase **object**, base de todo objeto de __Python__. Mientras que el método  `__init__` se ocupa inicializa todos los atributos y metodos de instancia.

Junto con la implementación de la metaclase **Meta** obtenida a largo de esta explicación y el siguiente ejemplo se evidencia 
el orden de todo el preoceso antes explicado: 

```python
class MyClass(metaclass = Meta):
    def __init__(self):
        print("MyClass.__init__() --> Inicializando al objeto")
    def __new__(cls): 
       x = super().__new__(cls)
       print("MyClass.__new__() --> Creando nueva instancia de la clase")
       return x

obj = MyClass()
```

```bash
$ python ejemplo.py
Meta.__new__() --> Creando una isntancia de la metaclase que controlará a la clase
Meta.__init__() --> Inicializando la metaclase
Meta.__call__() --> Creando una isntancia de la clase que controlará el objeto
MyClass.__new__() --> Creando nueva instancia de la clase
MyClass.__init__() --> Inicializando al objeto
Meta.__call__() --> Instancia de la clase que controlará al objeto ya creada
```
Otra de las posibles usos de las metaclase se como una fábrica de clases precisamente por el proceso antes descrito. Como cuando se crea un objeto, instancia de una clase, el mecanismo es una combinación con el método `__call__` de la metaclase resptectiva y los métodos `__init__` y  `__new__` de la clase entonces metidiante la modificación de estos métodos se le puden agregar 'cosas adicionales' a las clases, como registrar la nueva clase con algún registro o reemplazar la clase con algo completamente diferente.


# Patrón Singleton en Python

El patrón **Singleton** consiste básicamente en el control de las cantidad de instancias de una clase. Mediante el análisis detallado del mecanismo de __Python__ para crear la instancia de una clase se puede resumir que existen dos momentos claves en el mecanismo, uno antes de que se cree la nueva instancia y otro posterior a esta creación. Como el patrón **Singleton** consiste en limitar la cantidad de instancias que se pueden hacer de una clase entonces para la implentación consistente del patrón se debe intervenir en dicho mecanismo en el primer momento, antes de que se cree la nueva instancia. Este primer momento en concreto se refiere al métodos `__call__` de las metaclase en cuestión antes de llamar al método `__new__` de la clase que se intenta instanciar y a dicho método `__new__` antes de que llamé a este mismo método de su clase base, pues una vez se alcance el método 
`__new__` de **object** el interprete realizara todas las acciones necesarias para crear una nueva instancia de laclase en cuestión. A continuación ambas interupciones en los distintos métodos `__call__` y `__new__` para lograr una implementación consistente del patrón:

```python
class MetaSingleton(type):
    _instances = None
    def __call__(cls, *args, **kwargs):
        if cls._instances is None:
            cls._instances = super(MetaSingleton, cls).__call__(*args, **kwargs)
        return cls._instances
```

Esta primera implementación interrumpe el mecanismo de instanciación en el método `__call__` de la metaclase, que como se explicó anteriormente es el primer método que se llama cuando se decea crear un nuevo objeto o instancia. Como la implementación anterior es una metaclase para su utilización se debe emplear la sintaxis anteriormente descrita, `metaclass = MetaSingleton`. Notese además que una vez que una clase seleccione a **MetaSingleton** como su metaclase toda clase que que herede de dicha clase también tendrá esta implemetación como metaclase, a menos que explicitamente se escoja otra metaclase. Esta es una buena imlementación del patrón, pues modifica directamente la "manera tradiciona" de instanciar un objeto, pero como se comentó antes la utilización directa de las metaclase no es una práctica común y en la mayoria de los escenarios existen otras vias de solución: 

```python
class SingletonBase:
    _instance = None
    def __new__(cls,*args, **kwargs):
        if cls._instance is None:
            cls._instance = super().__new__(cls, *args, **kwargs)
        return cls._instance
```

Para eliminar el trato directo con las metaclases se puede interrumpir el mecanismo en el otro espacio del primer momento, antes de que se cree la nueva instancia, el método `__new__` de la clase que se decea instanciar. Notese que esta implementación también es reutilizable mediante la herencia, a menos que la clase que herede de **SingletoBase** modifique explicitamente su método `__new__`. Esta opción del patrón es ideal pues no se puede crear una instancia de una clase sin llamar al método `__new__` o sin implementar una nueva vía para llamar a los métodos `__new__` de las clases presendentes en la jerarquía. Aun así los autores prefieren la siguiente implementación:       

```python
def singleton(class_):
    instance = None
    origin_new = class_.__new__
    def decorated_new(*args, **kwargs):
        nonlocal instance
        if instance is None:
            instance = origin_new(*args, **kwargs)
        return instance
    class_.__new__ = decorated_new
    return class_

```
En esta última implementación se interrumpe el mecanismo en el mismo nivel, el método `__new__` de la clase en cuestión, pero presenta varios detalles de diseño que la posicionan como la mejor implementación. La primera es que se elimina la variable estática **_instance** del interior de las clases que vaya a seguir este patrón, pues en este caso dicha variable queda almacenada como resultado de la clausura del método **decorated_new**. Otra de las razones tienen que ver con el desacoplamiento de conceptos; en las anteriores implementaciones para su uso es necesario que la clase en su definición se marque como una clase que usará el patrón, ya se mediante la herencia o por la palabra reservada **metaclass**. En este caso aunque es cierto que se puede marcar a la clase en su definición de igual manera usando el decorador `@singleton`, no es estricatamente necesario para utilizar esta implementación. A continuación un ejemplo:  

```python
class Hero:
    pass

class SuperHero(Hero):
    pass

singleton(Hero)
h1 = Hero()
h2 = Hero()
print(h1 == h2) #true
h3 = type(h1)()
print(h1 == h3) #true
print(h2 == h3) #true
```

Notese que como en la segunda implementación el patrón es reusable mediante la herencia, lo que se debe a que obiamente es resultado final es el mismo una clase que contiene la lógica del patrón en su método `__new__`. Pero como antes se comentaba esta útima implementación puede aplicar el patrón a una clase en cualquier momento y no solo al tiempo de la definición de la clase en cuestión.






















 













 


  
    




