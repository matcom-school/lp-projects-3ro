from test2 import C

class Meta(type):
    def __call__(cls):
        x = super(Meta,cls)
        print(x)
        print("habalteeeeee")
        x.data = 100
        return x
    def __new__(cls, name,listt,dirr):
        x = super().__new__(cls,name,listt,dirr)
        print("consoltaaaaaaa")
        x.data_to = 100
        return x

class A(metaclass = Meta):
    pass

class B(metaclass = Meta):
    pass



x = A()

#print(A.__bases__)
#print(A.__dict__)
#print(A.__mro__)
#
#print(A.__subclasscheck__(type))
#print(A.__subclasscheck__(object))
#
#print(object.__subclasscheck__(A))
#print(object.__subclasscheck__(type))
#
#print(type.__subclasscheck__(type,A))
#print(type.__metaclass__)
#print(type.__base__)
#print(type.__mro__)