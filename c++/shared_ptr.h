#ifndef SHPTR_H
#define SHPTR_H

#include "control_block.h"
#include "iostream"
using namespace std;

template<class T>
class shptr
{
    private:
        _control_block<T>* cb;
    public:
        shptr() 
        {
            cout << "call shptr()" << endl;
            cb = nullptr;
        };
        shptr(_control_block<T>* obj) 
        { 
            cout << "call make_shptr" << endl;
            this->cb = obj; 
        };
        shptr(T * ptr) 
        {
            cout << "call shptr(T * ptr)" << endl;
            this -> cb = new cb_with_ptr<T>(ptr);
        };
        shptr(const shptr& other) {

            cout << "call ctor copy" << endl;
            other.cb -> inc();
            this -> cb = other.cb; 
        };

        void operator=(const shptr& shp)
        {
            cout << "call operator=" << endl;
            shp.cb->inc();
            this->cb = shp.cb;
        };
        
        
        T get_value() { return cb ->get_value(); };
        ~shptr(){
            cout << "call ~shptr()" << endl;
            if (cb == nullptr) 
                cout << "null" << endl;

            else if ( cb -> dec() == 0 )
                delete cb;
        };
};

template<class T, class ... Args > 
shptr<T> make_shptr (Args && ... args ){
    auto obj = new cb_with_obj<T>(args...);

    return shptr<T>(obj);
}

#endif