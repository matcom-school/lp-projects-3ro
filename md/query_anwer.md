# que es un autoalojamiemto del complilador 
## ultima oracion segundo parrafo de lisp

<!-- Defecto de fabrica del traductor -->
### Cambiar por : compilador autocontenido
Desde 1970 se ha convertido en una practica muy comun  escribir el compilador en el mismo lenguaje que  
este compila. Y aunque pareces antinatural tiene una explicacion muy sencilla. Generalmente, la primera versión del compilador se escribe en un idioma diferente, y luego cada versión posterior se escribe en ese idioma y se compila con la versión anterior. Una vez que haya compilado la versión x con la versión x-1, puede usar la versión recién creada x para recompilarse, aprovechando las nuevas optimizaciones que la versión presenta. GCC hace sus lanzamientos de esa manera. 
<!-- no se si poner que los compiladores son programas ya interpretados o complilados y que por tanto no necesitan compilarse cada vez que se ejecutan
 esto lo digo xq lo de la vercion x y la x-1 suena a super costoso pero en realidad es solo un nivel -->

## en lisp la parte del if no se entiende
<!-- Sustituir entero -->
Los "_operadores especiales_" (a veces llamados "_formas especiales_") proporcionan
la estructura de control de Lisp. Por ejemplo, el operador especial **if** toma tres
argumentos. Si el primer argumento no es **nil** , se evalúa como el segundo
argumento; de lo contrario, se evalúa al tercer argumento. Asi pues en el ejemplo siguente
como la primera expresión se evalua **nil** el operador especial evaluará la expresion que 
toma el lugar del segundo argumento, y tengra igual valor de retorno que este segundo argumento 

```lisp
  (if nil               ;; la expresión nill primer argumento 
    (list 1 2 "foo")    ;; la expresión (list 1 2 "foo") segundo argumento
    (list 3 4 "bar"))   ;; la expresión (list 3 4 "bar") tercer  argumento

    ;; [1,2,"foo"]
```

## A que se refieren con sistema lisp?
Cambiar sistema lisp por distribuciones de Lisp (Clojure,Common Lisp ,etc)


## que es una Estructura de listas?

<!-- Poner como penultimo parrafo del apartado
Sintaxis de Lisp 
cambiar estructura de lista por  _"estructura de lista"_-->

El elemento fundamental en Lisp es la lista, en el sentido más amplio del término, 
pues tanto los datos como los programas son listas. De ahí viene su nombre, pues 
Lisp es un acrónimo de "ListProcessing".Las listas en LISP están delimitadas por 
paréntesis. Estas listas son listas enlasadas implemetadas sobre la base de los 
pares **car** (Content of Address part of Register) y **cdr**
(Content of Decremental part of Register). Una de las caracteristicas mas reconocible
del lenguaje Lisp es su _"estructura de lista"_, pues sus programas se componen de 
secuencias de expresiones donde cada expresión tiene el formato de lista antes 
mencionado y son interprestadas como tal.  


## complilacion de lisp
Chama aqui no se que hacer xq todos hablan muy general xq hay de todo:
- versiones compliladas
- interpretada 
- interpretadas en un maquina virtual con un complilador 
mira este link 
https://stackoverflow.com/questions/12593768/how-is-lisp-dynamic-and-compiled
https://www.quora.com/Is-Common-Lisp-interpreted
http://tiborsimko.org/common-lisp-interpreted-compiled.html

papa sorry pero no me queda claro como pa escribirlo leetelo y si a ti te 
queda claro metele sino despues lo discutimos 

## lso commentarios en ingles 
tu los traduces mejor que yo deja la locura ajajajajaj