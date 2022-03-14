# Primitivas
En la implementación de esta tarea se utilizó como guía los ejemplos de la documentación
oficial de Microsoft. Implementando los métodos necesarios para lograr el efector lo más
similar posible a cuando se ejecutan con las primitivas de .NET

## Semaphores
Esta primitiva permite controlar el acceso a cierta zona de código que se considera 
crítica. Esta provee par de operaciones seguras para la delimitación de dichas zonas 
críticas. Ante el uso incorrecto de estos elementos se pueden dar situaciones que 
compromentan el funcionamiento del programa ejecutado, como por ejemplo cuando
los recursos adquiridos no son liberados tras su uso o cuando se utiliza un recurso
sin pedirlo antes.
.NET contine una implementación de esta prirmitiva, seleccionada en esta tarea como
base para el resto de las implementaciones. La clase Semaphore mediante su constructor
obtiene el máximo de elementos que pueden pasar a la zona crítica y provee los métodos 
WaitOne y Release como las operaciones seguras antes comentadas.

## Barriers
Las Barriers son utilizadas para delimitar en fases un programa. La misma dado un número N
de threads que se ejecuten en paralelo y se asegurar que todos bloquen en cada fase hasta 
que el resto de los N-1 threads hayan completado la misma fase. Si algunos de estos threads
por alguna razón no alzanza el final de una fase dada, los N threads quedan bloquedao en 
espera del permiso para seguir.

```csharp
    //Ejemplo de Microsoft
    Primitives.Barrier barrier = new Primitives.Barrier(3, (b) =>
    {
        Console.WriteLine("Post-Phase action: count={0}, phase={1}", count, b.CurrentPhaseNumber);
        if (b.CurrentPhaseNumber == 2) throw new Exception("D'oh!");
    });

    barrier.AddParticipants(2);
    barrier.RemoveParticipant();

    Action action = () =>
    {
        Interlocked.Increment(ref count);
        barrier.SignalAndWait();
        Interlocked.Increment(ref count);
    };
    
    Parallel.Invoke(action, action, action, action);
```

En función del ejemplo anterior se implemento la clase __Barrier__ con las siguientes 
operaciones:
 - Constructor: que espera el numero de participantes y un `Action<Barrier>` que se ejecutará
 al final de cada fase
 - CurrentPhaseNumber: propiedad que refleja el numero de fases sobrepasada hasta el momento
 - AddParticipants: método que dado un entero actualiza el número de participantes tras súperar
 la fase en curso
 - RemoveParticipant: método que disminuirá el número de participantes en uno tras súperar
 la fase en curso  
 - SignalAndWait: mecánismo para notificar el fin de la fase actual, para un threads en cuestión 

Además de usar la primitiva seleccionada en el funcionamiento de la clase. El uso más importate 
es en la implementación del método SignalAndWait, que según la cantidad de participantes que han 
llegado a la barrera en un momento dado. Si dicha cantidad es igual al número de de participantes 
realiza Release(canitidad de participantes) para dar continuidad a todos los threads. En el caso 
contrario se realiza WaitOne en cada threads por separado, bloqueando el mismo hasta que todos 
terminen la fase

## CountdownEvent

Esta primitiva detiene la ejecución de un threads a la espera de una cantidad de señales dadas.

```csharp
    //Ejemplo de Microsoft
    CountdownEvent cde = new CountdownEvent(10000);

    Action consumer = () =>
    {
        int local;
        while (queue.TryDequeue(out local)) cde.Signal();
    };

    Task t1 = Task.Factory.StartNew(consumer);
    cde.Wait();  
    Console.WriteLine("Done emptying queue.  InitialCount={0}, CurrentCount={1}, IsSet={2}",
        cde.InitialCount, cde.CurrentCount, cde.IsSet);

    await Task.WhenAll(t1, t2);

    cde.Reset(10);
    cde.AddCount(2);

    Console.WriteLine("After Reset(10), AddCount(2): InitialCount={0}, CurrentCount={1}, IsSet={2}",
        cde.InitialCount, cde.CurrentCount, cde.IsSet);
```

En función del ejemplo anterior se implemento la clase __CountdownEvent__ con las siguientes 
operaciones:
 - Constructor: que espera el numero de señales que va esperar el threads para desbloquerse
 - Signal: lanza una de las señales esperadas y si es la ultima de ellas, desata el protocolo 
 desbloqueo
 - Wait: bloquea la ejecución del threads en cuestión, hasta recibir las señales esperadas 
 - InitialCount: propiedad que representa la cantidad inicial de señales a recibir
 - CurrentCount: propiedad que representa la cantidad actual de señales recibidas 
 - IsSet: propiedad boleana que responde si se han recibido todas las señales o no
 - Reset: análogo al constructor de la clase
 - AddCount: método que dado un entero, aumenta en dicha cantidad el numero de señales esperadas  

## Monitors
Los Monitors permite sincronizar el acceso a las zonas críticas mediante llaves de bloqueos 
sobre distintas instancias. Estos cuentan con operaciones para acceder a una llave y poder
utilizar sin problema de concurrencia la instancia respectiva, además de otras para liberarla.
En .NET esta primitiva es una clase es estática.

```csharp
    List<Task> tasks = new List<Task>();
    Random rnd = new Random();
    long total = 0;
    int n = 0;
            
    for (int taskCtr = 0; taskCtr < 10; taskCtr++)
        tasks.Add(Task.Run( () => { int[] values = new int[10000];
                                    int taskTotal = 0;
                                    int taskN = 0;
                                    int ctr = 0;
                                    Primitives.Monitor.Enter(rnd);
                                    for (ctr = 0; ctr < 10000; ctr++)
                                        values[ctr] = rnd.Next(0, 1001);
                                    Primitives.Monitor.Exit(rnd);
                                    taskN = ctr;
                                    foreach (var value in values)
                                        taskTotal += value;

                                    Console.WriteLine("Mean for task {0,2}: {1:N2} (N={2:N0})",
                                        Task.CurrentId, (taskTotal * 1.0)/taskN, taskN);
                                    Interlocked.Add(ref n, taskN);
                                    Interlocked.Add(ref total, taskTotal);
                                } ));
    try {
        Task.WaitAll(tasks.ToArray());
        Console.WriteLine("\nMean for all tasks: {0:N2} (N={1:N0})",(total * 1.0)/n, n);
    }
    catch (AggregateException e) {
        foreach (var ie in e.InnerExceptions)
            Console.WriteLine("{0}: {1}", ie.GetType().Name, ie.Message);
```

Para lograr este comportamiento se creará una clase que posea un diccionario estático
`Dictionary<object, Monitor>` y con constructor privado para que solo se pueda modificar 
dicho diccionario mediante las operaciones estaticas Enter y Exit.  
- Enter: método estático que crea o accede al valor del diccionario, relativo a la 
llave dada como parametro. Con dicho valor se llaman a los métodos necesarios para 
proporcionar la explotación segura de la llave 
- Exit: método estático que accede al valor del diccionario, relativo a la 
llave dada como parametro. Con dicho valor se llaman a los métodos necesarios para 
indicar el fin de la explotación de la llave

# Problemas Clásicos

## Problema de los 5 filósofos

Se tiene N filósofos sentados alrededor de una mesa. Cada filósofo tiene un cubierto a
cada lado (uno a la izquierda y otro a la derecha). Para que uno filósofo en cuestión puedan 
comer necesita tener en esclusividad los cubiertos que tienen a cada lado a la vez. Se toma un 
cubierto, si y el otro está ocupado, se quedará esperando con el mismo en la mano hasta que el 
faltante sea liberado para comenzar a comer. Cuando un filósofo termina de comer libera ambos
cubiertos. En el caso en que todos los filósofos tomen el cubierto a su izquierda (o la derecha) 
a la vez se produce un bloqueo mutuo (deadlock), con lo cuale ninguno podrá comer. La cuestión 
es encontrar la manera de que todos coman, sin importar el orden 
    
    Solución: El problema ha sido modelado con una clase por cada filósofo, con un método por 
    cada estado por el que pasa un filósofo, y que referencia a los cubiertos adyacentes a este. 
    Y como método principal TryEat que simula el comportamiento del filósofo como describe el 
    problema y cambiando de estado de manera aleatoria. La implemntación pasa por la primitiva
    Monitor, en específico se usan los métodos TryEnter y Exit. Con el uso de TryEnter
    no es poisble caer en deadlock como se describe en del problema, ya que
    si en el período de tiempo dado no es posible bloquear el recurso, el mismo es liberado


## Problema del barbero dormilón

Un barbero cuenta con un único sillón para atender a los clientes y varias sillas 
para que los clientes esperen su turno. Cuando no hay clientes, el barbero se sienta
en una silla y se duerme. Cuando llega un nuevo cliente, este o bien despierta al
barbero o si el barbero está trabajando se sienta a esperar, o se va si todas las 
sillas de espera están ocupadas. 
Bajo esta descripción se puede dar un escenario donde el programa caé  en deadlock. 
Si el barbero está atendiendo a un cliente, mientras llega uno nuevo y se dirige a 
las sillas de esperar, justo en ese momento, el barbero termina y revisa las sillas 
de espera y aún el nuevo cliente no se ha sentado a esperar. En este caso  el barbero
se duerme y luego el cliente se sienta en una de las sillas de esperar, provocanddo 
deadlock. 

    Solución Usar primitivas para proteger las zonas crítica, para asegurar que el
    barbero y la entrada de los clientes no funcionen al mismo tiempo. En concreto
    en este caso se usaro los semáforos