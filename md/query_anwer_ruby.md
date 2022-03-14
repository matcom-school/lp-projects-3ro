## Errores graves de concepto
<!--Papa me da la impresion de que se parrafo habla en su interior 
de que los lenguages complilados no se puede hacer una consola interactiva
asi de facil. Ni idea que fue lo que me paso jajajajajaj -->
Solucion tumbarlo al final era una mera introducion

Quizás la característica más apreciada de Ruby es la metaprogramación,
con la cual se puede manipular el lenguaje para así lograr disímiles 
objetivos, en lugar de adaptarse al lenguaje cómo es, es por ello 
que la metaprogramación y los DSLs están tan relacionados en Ruby.
Para la metaprogramacion _Ruby_ se vale de las clases `Kernel`, `Object` y `Module`
que definen métodos con el mismo objetivo que `System.Reflection` en _C#_, trabajar
con los metadatos del programa en ejecución. 

Otra de la características que hacen de Ruby un lenguage tan flexible e ideal 
para el desarrollo de DSLs es la posibilidad de continuar la implementacion de 
sus clases base, es en un momento dado se puede escribir el codigo del ejemplo 
siguiente logrando asi sumar la propiedad **factorial** al  tipo basico de Ruby
**Interge**

```ruby
class Integer
     def factorial
        if self < 0
            return 'You can\'t take the factorial of a negative number!'
        end
        if self <= 1
            1
        else
            self * (self - 1).factorial
        end
    end
end

puts 5.factorial ;; 120 
```
Y aunque este objetivo tambien se puede lograr con la metaprogramacion, es una 
caracteristica muy util cuando se quiere agregar una cantidad considerable de 
metodos y propiedades. Este feature se puede lograr en C# con los metodos 
extensores   
<!--En Python no se espero que tu si-->


Pero gracias al tipado dinámico de Ruby, la posibilidad de extender sus tipos
de manera facil y elegante (incluido los tipos principales) y este trabajo con 
los metadatos Ruby es uno de los lenguajes referencia al momento de hablar sobre 
DSLs. A continuación algunos ejemplos: