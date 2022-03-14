#include <iostream> 
using namespace std;

#define for_x_in_range(len) for(int i = 0; i < len; i ++)
#define lcomp(action,_for) _for ## action;
#define print(value) cout<<value<<endl;

int main () {
    lcomp(print(i), for_x_in_range(10)))
   return 0;
}