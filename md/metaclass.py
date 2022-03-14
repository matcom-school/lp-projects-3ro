class Singleton(type):
    _instances = {}
    def __call__(cls, *args, **kwargs):
        if cls not in cls._instances:
            cls._instances[cls] = super(Singleton, cls).__call__(*args, **kwargs)
        return cls._instances[cls]

class Person(metaclass=Singleton):
    def __init__(self):
        self.name = "Ricardo"
        
print("-------------Metaclases-------------")
p1 = Person()
p2 = Person()
print(p1 == p2) #true
p3 = type(p1)()
print(p1 == p3) #true
print(p2 == p3) #true
p1.name = "Other"
p2.name = "Andy"
print(p1.name) #Andy
print(p2.name) #Andy
print(p1.name == p2.name) #true