ó í é á ¿
### a) **F** 
Aunque el programador tiene la responsabilidad de manejar y controlar 
la memoria reservada en el heap. Pero el copilador se encarga de 
liberar los demás recursos empleados por el programa en cuestión. 
Con lo cual no se puede hablar de un manejo **total** de la memoria

### b) **F**
_Python_ es fuertemente típado. En _Python_ no se permite la conversión
implícita de típos no realcionados, sino que se debe hacer uso de las 
herramientas de casteo aportada por el lenguaje o por la definición del 
típo(clase). Por ejemplo, en _Python_ no se puede realizar operaciones 
entre los típos `int` y `str` a menos que se use explícitas o procesos
que impliquen dicha trasformación

```python
a = 4
b = "5" 
# casteo explícito 
c = str(4) + 5 # c = "45"
c = 4 + int(5) # c = 9

#proceso que implica el casteo explícito 
print(a) 
c = f'C-{ a }12'
# estos mecanismo usan el casteo definido por el tipo int 
# con el metodo "magico" __str__

print(a + b) #TypeError: unsupported operand type(s) for +: 'int' and 'str'
```
### c) **V**
En _C#_ las variables "capturadas" pasan a ser miembro de una nueva clase 
que se define al momento de la clausura, dichos miembos son referecias que 
apuntan a las direcciones de memoria de las variables "capturadas". Como el 
GCollector no libera la memoria que este siendo referenciada, entonces en el
cuerpo de la clasura todos los valores siempre seran "seguros". Sin embargo
en C++ en una clausura similar (con variables "capturadas" por referencia)
puede darse el caso en que aparezcan comportamientos inesperados; debido a 
que se puede haber liberado la memoria que almacena dicha variable, ya sea 
por desición del programador o del compilador            

### d) **F**
La palabra clave `dynamic` es utilizada para indicar que una instancia esta
involucrada en un _**enlace tardío** (Late Binding)_ y que el **_DLR_** o
_Dynamic Language Runtime_ se encargue del manejo de este objeto. El
comportamiento de este objeto durante el _enlace tardío_ puede ser controlado y
sobreescrito a traves de la implementación de la interfaz
`IDynamicMetaObjectProvider`, el **_DLR_** se encargará de llamar a los métodos
provenientes de `IDynamicMetaObjectProvider`, los cuales describen el comportamiento
de la clase en el momento de enlace.
Para otros escenarios donde la palabra clave `dynamic` no es suficiente, 
_DLR_ proporciona algunas clases que implementan `IDynamicMetaObjectProvider`.
   


