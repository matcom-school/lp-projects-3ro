## Lisp
Lisp (históricamente LISP ) es una familia de lenguajes de programación con una larga historia y una notación de prefijo distintiva, completamente entre paréntesis. Originalmente especificado en 1958, Lisp es el segundo lenguaje de programación de alto nivel más antiguo en uso generalizado en la actualidad. Solo Fortran es mayor, por un año.

Lisp fue creado originalmente como una práctica notación matemática para los programas de ordenador. Como uno de los primeros lenguajes de programación, Lisp fue pionera en muchas ideas en ciencias de la computación , incluyendo estructuras de árbol de datos , gestión de almacenamiento automático , tipado dinámico , condicionales , funciones de orden superior , la recursividad , la autoalojamiento compilador , y el ciclo de lectura – evaluación – impresión .

Las listas enlazadas son una de las principales estructuras de datos de Lisp, y el código fuente de Lisp está hecho de listas. Por lo tanto, los programas Lisp pueden manipular el código fuente como una estructura de datos, dando lugar a los macro sistemas que permiten a los programadores crear nuevas sintaxis o nuevos lenguajes específicos de dominio integrados en Lisp.

### Sintaxis de Lisp
Lisp es un lenguaje orientado a la expresión y por tanto no se hace distinción entre "expresiones" y "declaraciones"; todo el código y los datos se escriben como expresiones. Cuando se evalúa una expresión, produce un valor, que luego se puede incrustar en otras expresiones. Cada valor puede ser cualquier tipo de datos.

El uso de paréntesis es la diferencia más obvia de Lisp de otras familias de lenguaje de programación y un rasgo que en ocaciones resulta incomodo. Pero, la sintaxis de la expresión S también es responsable de gran parte del poder de Lisp: la sintaxis es extremadamente regular, lo que facilita la manipulación por computadora.

Las funciones de Lisp se escriben como listas, se pueden procesar exactamente como los datos. Esto permite escribir fácilmente programas que manipulan otros programas ( metaprogramación ). Muchos dialectos de Lisp explotan esta característica utilizando sistemas macro, que permiten la extensión del lenguaje casi sin límite.

#### Listas 

Una lista de Lisp se escribe con sus elementos separados por espacios en blanco y entre paréntesis. La lista vacía () también se representa como el átomo especial **nil**. Esta es la única entidad en Lisp que es tanto un átomo como una lista.
#### Expresiones

Las expresiones se escriben como listas, usando la notación de prefijo . El primer elemento en la lista es el nombre de una función, el nombre de una macro, una expresión lambda o el nombre de un "operador especial". El resto de la lista son los argumentos. Por ejemplo, la función list devuelve sus argumentos como una lista:

``` lisp
( list  1  2  ( quote  foo ))
```

Cualquier expresión sin comillas se evalúa de forma recursiva antes de evaluar la expresión de cierre.

``` lisp
( lista  1  2  ( lista  3  4 ))
``` 

AL evaluar esta expresión tenga en cuenta que el tercer argumento es una lista osea (1 2 (3 4))

#### Operadores 
Los operadores aritméticos se tratan de manera similar a las expresiones

 ``` lisp
 ( +  1  2  3  4 )
 ```

Lisp no tiene noción de operadores, los operadores aritméticos en Lisp son funciones variadas (o n-arias ), capaces de tomar cualquier número de argumentos. Un operador de incremento '++' de estilo C a veces se implementa bajo el nombre **incf** dando sintaxis

 ``` lisp
 ( incf  x )
 ``` 
 
 lo cual equivale a: 
 
 ``` lisp
 (setq x (+ x 1))
 ``` 

Los "operadores especiales" (a veces llamados "formas especiales") proporcionan la estructura de control de Lisp. Por ejemplo, el operador especial **if** toma tres argumentos. Si el primer argumento no es **nil** , se evalúa como el segundo argumento; de lo contrario, se evalúa al tercer argumento. Así, la expresión

 ``` lisp 
    ( if  nil 
        ( list  1  2  "foo" ) 
        ( list  3  4  "bar" ))
``` 

Lisp también proporciona operadores lógicos. Los operadores **and*** y **or** realizan una evaluación de cortocircuito y devolverán su primer argumento nil y non-nil respectivamente.

``` lisp
    (or (and "zero" nil "never") "James" 'task 'time)
``` 

#### Expresiones lambda y definición de funciones 

Otro operador especial **lambda**, se utiliza para vincular variables a valores que luego se evalúan dentro de una expresión. Este operador también se utiliza para crear funciones: los argumentos lambda son una lista de argumentos y la expresión o expresiones a las que se evalúa la función

 ``` lisp
    ( lambda  ( arg )  ( +  arg  1 ))
```

Las expresiones lambda no se tratan de manera diferente a las funciones nombradas; se invocan de la misma manera. Por lo tanto, la expresión

``` lisp
    (( lambda  ( arg )  ( +  arg  1 ))  5 )
```  

evalúa a 6. A este caso se le conoce como **función anónima**.Las funciones con nombre se crean almacenando una expresión lambda en un símbolo utilizando la macro **defun** .

```lisp
    ( defun  foo  ( a  b  c  d )  ( +  a  b  c  d ))
```

De esta manera se define una nueva función nombrada foo el entorno global.Otra forma de lograrlo es 
```lisp
    (setf (fdefinition 'f) #'(lambda (a) (block f b...)))
```

### Estructura de lista del código del programa, explotación por macros y compiladores 

Una distinción fundamental entre Lisp y otros lenguajes es que en Lisp, la representación textual de un programa es simplemente una descripción legible por humanos de las mismas estructuras de datos internas (listas vinculadas, símbolos, números, caracteres, etc.) que usarían el sistema subyacente Lisp.

Lisp usa esto para implementar un sistema macro muy poderoso. Al igual que otros lenguajes de macros como C , una macro devuelve código que luego se puede compilar. Sin embargo, a diferencia de las macros C, las macros son funciones de Lisp y, por lo tanto, pueden explotar todo el poder de Lisp.

Además, debido a que el código Lisp tiene la misma estructura que las listas, las macros se pueden construir con cualquiera de las funciones de procesamiento de listas en el lenguaje. En resumen, cualquier cosa que Lisp pueda hacer a una estructura de datos, las macros de Lisp pueden hacer al código. En contraste, en la mayoría de los otros idiomas, la salida del analizador es puramente interna a la implementación del lenguaje y el programador no puede manipularla.

En implementaciones simplistas de Lisp, esta estructura de lista se interpreta directamente para ejecutar el programa; Una función es literalmente una parte de la estructura de la lista que el intérprete recorre al ejecutarla. Sin embargo, los sistemas Lisp más importantes también incluyen un compilador. El compilador traduce la estructura de la lista a código de máquina o bytecode para su ejecución. Este código puede ejecutarse tan rápido como el código compilado en lenguajes convencionales como C.
 
### Macros de Lisp y DSL

Las macros se utilizan para definir extensiones de sintaxis de lenguaje para Common Lisp o Idiomas específicos de dominio (DSL). Estos idiomas están integrados directamente en el código Lisp existente. Ahora, los DSL pueden tener una sintaxis similar a Lisp  o completamente diferente.

Aquí hay un ejemplo más concreto:
Python tiene comprensiones de listas integradas en el lenguaje. Esto proporciona una sintaxis simple para un caso común. La línea
``` python
divisibleByTwo = [x for x in range(10) if x % 2 == 0]
```

produce una lista que contiene todos los números pares entre 0 y 9.
En Lisp podríamos definir DSL para hacer comprensiones de listas. De hecho, ya estamos usando una macro.

Lisp define un par de formas de sintaxis especiales. 
- ( **'**) indica que el siguiente token es literal.
- ( **`**) indica que el siguiente token es un literal con exepciones. Las exepciones estan presedidas por el operador de coma. 
El literal '(1 2 3)es el equivalente de Python [1, 2, 3]. Puede asignarlo a otra variable o usarlo en su lugar. Puede pensar (1 2 ,x)como el equivalente de Python [1, 2, x]donde xes una variable previamente definida. Esta notación de la lista es parte de la magia que entra en las macros. La segunda parte es el lector Lisp que sustituye inteligentemente las macros por el código, pero que se ilustra mejor a continuación:

Entonces podemos definir una macro llamada lcomp(abreviatura para la comprensión de la lista). Su sintaxis será exactamente como la pitón que usamos en el ejemplo [x for x in range(10) if x % 2 == 0] :(lcomp x for x in (range 10) if (= (% x 2) 0))
```lisp
    (defmacro lcomp (expression for var in list conditional conditional-test)
      (let ((result (gensym))) ;; create a unique variable name for the result
        ;; the arguments are really code so we can substitute them 
        ;; store nil in the unique variable name generated above
        ` (let ((,result nil))
           ;; var is a variable name
           ;; list is the list literal we are suppose to iterate over
           (loop for ,var in ,list
                ;; conditional is if or unless
                ;; conditional-test is (= (mod x 2) 0) in our examples
                ,conditional ,conditional-test
                ;; and this is the action from the earlier lisp example
                ;; result = result + [x] in python
                do (setq ,result (append ,result (list ,expression))))
               ;; return the result 
           ,result)))
```
Ahora podemos ejecutar en la línea de comando:

```lisp
  (let ((,result nil)(lcomp x for x in (range 10) if (= (mod x 2) 0))
```
Y de la misma manera se puede obtener cualquier sintaxis que pueda desear. Como Python o la  C #. O la sintaxis LINQ de .NET. Al final

