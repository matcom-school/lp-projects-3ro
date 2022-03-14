#class Meta(type):
#    def __add__(self,other):
#        class AutoClass(self,other):
#            pass
#        return AutoClass
#
#class A(metaclass = Meta):
#    pass
#
#class B:
#    pass
#
#
#auto_class = A + B
#auto_instance = (A + B)()
#print(auto_class.__name__)
#print(auto_class.__bases__)
#print(auto_instance)
#print(type(auto_instance))
#
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
    @staticme

print(MyClass.print_class_metaclass())
print(MyClass)
print(f'class:{MyClass.__name__} and metaclass: {MyClass.metaclass} ')
print(MyClass + MyClass)