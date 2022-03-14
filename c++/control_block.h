#ifndef CBLOCK_H
#define CBLOCK_H

#include "iostream"
using namespace std;

template<class T>
class _control_block
{
    private: 
        long counter;
    public:
        _control_block() : counter(1) {
            cout << "counter = " << counter << endl;
        };
        void inc() 
        {
            this->counter++;
            cout << "counter = " << counter << endl;

        }
        int dec() 
        {
            counter --;
            cout << "counter = " << counter << endl;
            
            if (!counter) {
                this -> dispose();
                return 0;
            }

            return 1;
        }

        virtual void dispose() = 0;
        virtual T get_value() = 0;
};

template<class T>
struct cb_with_obj : _control_block<T>
{
    T ptr;
    template<class ... Args>
    cb_with_obj( Args && ... args ) : ptr(args...) {};
    virtual void dispose() {};
    virtual T get_value() { return ptr; };
};

template<class T>
struct cb_with_ptr : _control_block<T>
{
    T* ptr;
    cb_with_ptr( T* _ptr ) : ptr(_ptr) {};
    virtual T get_value() { return *ptr; };
    virtual void dispose() 
    {
        delete ptr; 
    };
};

#endif