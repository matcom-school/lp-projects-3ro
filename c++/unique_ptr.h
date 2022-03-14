#ifndef UQPTR_H
#define UQPTR_H

#include "control_block.h"
#include "iostream"
using namespace std;


template<class T>
class uqptr
{
    private:
        cb_with_obj<T>* ptr;
    public:
        uqptr (cb_with_obj<T>* obj) : ptr(obj) {}
        uqptr (uqptr && other) 
        { 
            this -> ptr = other.ptr;
            other.ptr = nullptr;
            cout << "call ctr move" << endl;
        }
        T *operator->() { return &ptr -> ptr; }
        
        bool operator==(nullptr_t  other) {
            return this -> ptr == nullptr;
        }   
        ~uqptr()
        {
            if (this->ptr != nullptr) 
                this -> ptr -> dec();
            cout << "~uqptr()" << endl; 
        }

};

template<class T, class ...  Args>
uqptr<T> make_uqptr(Args && ... args)
{
    auto temp = new cb_with_obj<T>(args...);
    return uqptr<T>( temp );
};


#endif