# dynamic keyword
Cuando usa la palabra clave dynamic para interactuar con una instancia de un objeto, se le esta haciendo saber al complilador que
a esta instancia hay que un enlace tardio(Late binding) y que el DLR se encarge del manejo de este objeto. La interfaz IDynamicMetaObjectProvider se puede hacer que una clase tome el control de su comportamiento de enlace tardío.
Cuando utiliza la dynamic palabra clave para interactuar con una clase que implementa IDynamicMetaObjectProvider, el DLR llama a los métodos del IDynamicMetaObjectProvider los cuales deben describir el comportamiento de dicha clase bajo un enlace tardío.

# DLR?

## CLR or Common Language Rutime
El Common Language Runtime o CLR es un entorno de ejecución para los códigos de los programas que corren sobre la plataforma Microsoft .NET. El CLR es el encargado de compilar una forma de código intermedio (el conocido IL) a código de maquina nativo, mediante un compilador en tiempo de ejecución. No debe confundirse el CLR con una máquina virtual, ya que una vez que el código está compilado, corre nativamente sin intervención de una capa de abstracción sobre el hardware subyacente.
La manera en que la máquina virtual se relaciona con el CLR permite a los programadores ignorar muchos detalles específicos del microprocesador que estará ejecutando el programa. El CLR también permite otros servicios importantes, incluyendo los siguientes: 
-  Administración de la memoria 
-  Administración de hilos 
-  Manejo de excepciones 
-  Recolección de basura 
-  Seguridad

## DLR or Dynamic Language Rutime
El DLR (dynamic language rutime) agrega un conjunto de servicios al CLR para un mejor soporte de lenguajes dinámicos. Estos servicios incluyen lo siguiente:
-  Árboles de expresión. El DLR usa árboles de expresión para representar la semántica del lenguaje. Para este propósito, el DLR ha ampliado los árboles de expresión LINQ para incluir el flujo de control, la asignación y otros nodos de modelado de lenguaje.
- Interaccion y almacenamiento en caché. Mediante el dynamic call site, en un lugar en el código donde realiza una operación como a + b o a.b() en objetos dinámicos. El DLR almacena en caché las características a y b (generalmente los tipos de estos objetos) e información sobre la operación. Si dicha operación se ha realizado previamente, el DLR recupera toda la información necesaria de la memoria caché para un envío rápido.
- Interoperabilidad dinámica de objetos. El DLR proporciona un conjunto de clases e interfaces que representan objetos y operaciones dinámicos y pueden ser utilizados por implementadores de lenguaje y autores de bibliotecas dinámicas. Estas clases e interfaces incluyen IDynamicMetaObjectProvider , DynamicMetaObject , DynamicObject y ExpandoObject.

# Late binding?
## Termino enlace 
    Se le denomina enlace a la asociación de una función con su objeto correspondiente objeto al momento del llamado de la misma.

## enlace de tiempo de compilación, estatico o temprano 
    EL enlace temprano es el de una función miembro, que se llama dentro de un objeto dicho enlace se resuelve en tiempo de compilacion. Todos los métodos que pertenecen a un objeto o nombre de una clase (staticos) son a los que se pueden realizar enlaze de tiempo de compilación

## enlace tardío o dinamico
    El enlace tardío es cuando solo se pueder a que objeto A pertenece la función B en el tiempo de ejecución. Uno de los ejemplos mas comunes de este enlace son los metodos virtuales.

## Enlace tardío con métodos dinámicos vs. virtuales en C #
Los métodos virtuales todavía están "vinculados" en tiempo de compilación. El compilador verifica la existencia real del metodo y su tipo de retorno, y el compilador fallará al compilar si el método no existe o existe inconsistencia de tipos.
El método virtual permite el polimorfismo y una forma de enlace tardío, ya que hace que el método se enlaza al tipo adecuado en tiempo de ejecución a través de la tabla de métodos virtuales.
Dynamic es un animal diferente: con Dynamic, no hay absolutamente ningún enlace en el momento de la compilación. El método puede o no existir en el objeto de destino, y eso se determinará en tiempo de ejecución.
Todo es una cuestión de terminología: la dinámica es realmente un enlace tardío (búsqueda del método de tiempo de ejecución), mientras que virtual proporciona el envío del método de tiempo de ejecución a través de una búsqueda vitual, pero todavía tiene algún "enlace temprano" en su interior.

# Como se logra el comportamiento dinamico en C#
Este comportamiento es concecuencia directa del desarrolo del DLR el cual fue concebido para admitir las implementaciones "Iron" de los lenguajes de programación Python y Ruby en .NET Framework.
En en centro del entorno de ejecución DLR se posiciona la clase llamada DynamicMetaObject. Dicha clase implemeta los siguientes metodos para dar respuesta a como actuar en todos los posibles esenarios en los que se puede encontrar una instancia de un objecto en un momento dado:
 - BindCreateInstance: crea o activa un objeto
 - BindInvokeMember: llamar a un método encapsulado
 - BindInvoke: ejecuta el objeto (como una función)
 - BindGetMember: obtenga un valor de propiedad
 - BindSetMember: establece un valor de propiedad
 - BindDeleteMember: eliminar un miembro
 - BindGetIndex: get el valor en un índice específico
 - BindSetIndex: establece el valor en un índice específico
 - BindDeleteIndex: elimina el valor en un índice específico
 - BindConvert: convierte un objeto a otro tipo
 - BindBinaryOperation: invoque un operador binario en dos operandos suministrados
 - BindUnaryOperation: invoque un operador unario en un operando suministrado 

De manera general las clases definidas de manera ordinaria (estatica) saben como reacionar en dichos esenarios. 
Pero las clases dinamicas no tiene estas reaciones predefinidas por lo cual es necesario predefinir par estas clases su propio DynamicMetaObject el cual en tiempo de ejecucion sepa que tiene que ejecutar en cada esenario. 
Para definir una clase dinamica System.Dynamic proveé la interfaz IDynamicMetaObjectProvider el cual contiene el metodo:
   
        DynamicMetaObject GetMetaObject(Expression parameter)

El cual debe encargarse de retornar el DynamicMetaObject que describa el comportamiento de la clase dinamica que implemeta la interfaz IDynamicMetaObjectProvider según el árbol de expresiones que dicho método recive como parámetro 

# System.Dynamic.DynamicObject
Como anteriormente se puede observar en principio lograr un comportamieto dinámico en C# pasa por crear estos DynamicMetaObject y tener conocimientos para trabajar sobre el árbol de expresiones de C#. Para evitar todo este proceso System.Dynamic proveé la clase DynamicObject, pensada para poder definir comportamietos dinamicos abstraidos de todo el proceso anteriormente descrito pues ya cuenta un una implemetacion del metodo GetMetObject de IDynamicMetaObjectProvider. Dicha imlpemetacion relaciona los siguentes metodos a sus respectivos esenarios:

        public virtual bool TryBinaryOperation(BinaryOperationBinder binder, object arg, out object result)

Proporciona implementación para operaciones binarias. Las clases derivadas de la clase DynamicObject pueden anular este método para especificar el comportamiento dinámico para operaciones como la suma, multiplicación, etc. La clase BinaryOperationBinder
contiene una ExpressionType con información de la operación en se realiza al momento de llamado de esta función. Este metodo considera que la instancia de la clase derivada de DynamicObject es el operador de la derecha y arg es el de la izq.
        
        public virtual bool TryConvert(ConvertBinder binder, out object result)

Proporciona implementación para operaciones de conversión de tipos. Las clases derivadas de la clase DynamicObject pueden anular este método para especificar el comportamiento dinámico de las operaciones que convierten un objeto de un tipo a otro. La clase ConvertBinder contiene información sobre del tipo al cual se esta tratando de hacer la converción e incluso contiene informacion sobre si el proceso es explícito o implícito
        
        public virtual bool TryGetIndex(GetIndexBinder binder, object[] indexes, out object result)
        public virtual bool TrySetIndex(SetIndexBinder binder, object[] indexes, object value)

Proporciona la implementación para operaciones que obtienen(establecen) valores de miembros por índices. Las clases derivadas de la clase DynamicObject pueden anular este método para especificar el comportamiento dinámico para operaciones tales como obtener (acceden) un valor para una propiedad mediante unos índices específico.

        public virtual bool TryGetMember(GetMemberBinder binder, out object result)
        public virtual bool TrySetMember(SetMemberBinder binder, object value)

Proporciona la implementación para operaciones que obtienen (establecen) valores de miembros. Las clases derivadas de la clase DynamicObject pueden anular este método para especificar el comportamiento dinámico para operaciones tales como obtener (establecer) un valor para una propiedad. La clase GetMemberBinder (SetMemberBinder) contiene infoemacion sobre el nombre de la preopiedad en cuestión.

        public virtual bool TryInvokeMember(InvokeMemberBinder binder, object[] args, out object result)
    
Proporciona la implementación para operaciones que invocan a un miembro. Las clases derivadas de la clase DynamicObject pueden anular este método para especificar el comportamiento dinámico para operaciones como llamar a un método. La clase InvokeMemberBinder contiene infoemacion sobre el nombre de la miembro en cuestión y args es un array con los parámetros.

        public virtual bool TryInvoke(InvokeBinder binder, object[] args, out object result)

Proporciona la implementación para operaciones que invocan un objeto. Las clases derivadas de la clase DynamicObject pueden anular este método para especificar el comportamiento dinámico para operaciones como invocar un objeto o un delegado. La clase InvokeBinder contiene informacion sobre la cantidad de argumentos y sus nombres mediante una propiedad del tipo CallInfo y args son los parámetros 

# System.Dynamic.ExpandoObject

Aunque la clase DynamicObject es una gran abstración del proceso base, para su utilización es necesario implementar una clase que herede de el y realice los override necesarios lo cual es demasiado verboso en los casos más sencillos. Suponiendo que solo se necesita de un objeto dinámico que permita un control dinámico de propiedades, mediante DynamicObject necesitamos implementar los metodos TryGetMember y TrySetMember. Para evitar esto System.Dynamic proveé la clase ExpandoOject, la misma es una clase sealed y por tanto la misma no se puede extender. ExpandoObject implementa las interfaces IDictionary<KeyValuePair<string, object>> y IDynamicMetaObjectProvider entre otras. Mediante las dos interfaces antes mencionadas dicha clase logra el manejo dinámico de las propiedades. El proceso es realmente sencillo puesto que en su interios contiene algun tipo de implementacion de diccionario, al momento de asignar de una propiedad guarda el nombre de la propiedad como llave y el objeto que se le esta asignando como valor . Y en el momento en que se hace referencia a una propiedad busca si el nombre de dicha propiedad se encuentra entre las llaves, en caso afirmativo se devuelve el valor correspondiente de lo contrario se lanza un excepción     