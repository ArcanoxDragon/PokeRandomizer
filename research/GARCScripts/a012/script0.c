void func_0078(int arg1) {
    switch (arg1) {
        case -1:
            NATIVE_2();
            break;
        case 0:
            func_0120();
            break;
        case 1:
            func_01A4();
            break;
        case 2:
            func_0244();
            break;
        default:
            func_029C();
            break;
    }
    
    return;
}

void func_0120() { }

void func_012C( int a1, int a2 ) {
    NATIVE_2();
    int v1 = NATIVE_0( 0x4070 );
    
    if ( v1 ) {
        NATIVE_3( a1, a2 );
    }
    
    return;
}

void func_01A4() { 
    int v1 = NATIVE_5();
    
    if ( v1 ) {
        NATIVE_2();
        int v2 = NATIVE_1( 1, 0x4070 );
        func_012C( v2, 0 );
    } else {
        NATIVE_2();
        NATIVE_1( 0, 0x4070 );
    }
    
    func_0004();
    NATIVE_6( 13, 0X00f0 );
    NATIVE_2();
    
    return;
}

void func_0244() {
    NATIVE_2();
    NATIVE_0( 0x4072 );
    
}