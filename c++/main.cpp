#include <cstdio>
#include <iostream>
#include <memory>
#include "shared_ptr.h"
#include "unique_ptr.h"

using namespace std;

class A{
    private:
    public:
        int index;
        bool real;
        A(int i, bool b): index(i), real(b){}
};

uqptr<A> test_func() { return make_uqptr<A>(3,false); }

void unique_func()
{
    auto temp = test_func();
    cout << "the firts unique_ptr value inner int " << temp -> index << endl;
    cout << "the firts unique_ptr value inner bool " << temp -> real << endl;
    
}

void unique_test()
{
    auto ptr = make_uqptr<A>(10, true);
    cout << "the firts unique_ptr value inner int " << ptr -> index << endl;
    cout << "the firts unique_ptr value inner bool " << ptr -> real << endl;
    auto newptr = move(ptr);
    cout << "the second unique_ptr value inner int " <<newptr -> index << endl;
    cout << "the second unique_ptr value inner bool " <<newptr -> real << endl;
    if (ptr == nullptr)
        cout << "the firts unique_ptr is nullptr " << endl;

}

void make_test()
{
    shptr<int> a;
    auto b = make_shptr<int>(5);

    shptr<int> c = b;
    a = b;

    cout << "ptr value " << a.get_value() << endl;
}

void new_test()
{
    shptr<int> a;
    int* f =new int(5) ;
    auto b = shptr<int>(f);
    shptr<int> c = b;
    a = b;

    cout << "ptr value " << a.get_value() << endl;
}

int main()
{
    cout <<"make test"<< endl;
    cout << endl;
    make_test();
    cout << endl;
    
    cout <<"new test"<< endl;
    cout << endl;
    new_test();
    cout << endl;

    cout <<"unique test"<< endl;
    cout << endl;
    unique_test();
    cout << endl;

    cout <<"unique func"<< endl;
    cout << endl;
    unique_func();
    cout << endl;

    return 0;
}

