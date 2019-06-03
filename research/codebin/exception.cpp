#include "common.h";
#include "exception.h";

void throw_exception() {
    char* p0;
    void (*f1)(char*);
    
    while (1) {
        f1 = byte_5D1144[0x6C]; // seems to be a table of function pointers? exception handlers maybe?
        
        if (f1 != NULL) {
            p0 = unk_563574;
            f1(p0);
        }
    }
}