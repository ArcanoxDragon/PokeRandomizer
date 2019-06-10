#ifndef __AMXFUNCTION
#define __AMXFUNCTION

class AmxFunction {
    char flag0;
    char gap[3];
    int funcAddr;
    int textGarcFile;
    int textGarcSubfile;
    int locationMetadata;
    short locationIndex;
    short nameIndex;
    char* scriptName;
    short amxIndex;
    
public:
    AmxFunction(short, int, char*);
};

#endif