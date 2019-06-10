int* sub_1173DC(int* ptr, int a1) {
    int r2 = *ptr;
    
    if ( r2 ) {
        short r3 = *(short*)(r2 + 2);
        
        if ( r3 <= a1 ) {
            return;
        }
    }
    
    return *(int*)(r2 + 4 + a1 * 4) + r2;
}

int* get_script_unk_data(char* dataRaw, int index) {
    const int offset = 0x94C0;
    
    int* data = (int*) dataRaw;
    
    if ( *data != 0 ) {
        return *(data + offset + index);
    }
    
    if ( index != 0 ) {
        int r1 = *(data + offset + index);
        
        return sub_1173DC( &r1, 0 );
    }
    
    return sub_1173DC( data + 2, 0 );
}

short get_script_text_subfile(char* zoneFile) {
    return *(short*)(get_script_unk_data(zoneFile, 0) + 6);
}