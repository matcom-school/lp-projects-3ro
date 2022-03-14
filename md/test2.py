class Meta(type):
    def __init__(cls, name, _list, _dir):
        print("Hablateeeee")

    def __call__(cls):
        print("Creando una isntancia la clase que controlara el objeto")
        # antes de crear la instancia de la clase 
        x = super(Meta,cls).__call__() # llamando al metodo __call__ de type base de cls instancia especifica de la metaclase Meta
        # despues de crear la instacian de la clase
        print("Isntancia de la clase que controlara al objeto ya creada")
        return x #retornar la instancia de la clase 
    def __new__(cls, name, _list, _dir):
       # antes de crear la instancia de la metaclase 
       x = super().__new__(cls,name, _list, _dir) # llamando al metodo __new__ de type
       # despues de crear la instacian de la metaclase
       print("Creando una isntancia la metaclase que controlara a la clase")
       return x #retornar la instancia de la metaclase 

class MyClass(metaclass = Meta):
    def __init__(self):
        print("Inicializando al objeto")
    def __new__(cls): 
       x = super().__new__(cls)
       print("Creando nueva instancia de la clase")
       return x 

x = MyClass()