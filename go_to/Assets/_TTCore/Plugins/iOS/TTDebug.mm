#import "TTDebug.h"

#pragma mark - TTDebug

@implementation TTDebug
+ (BOOL)IsDebug {
    NSDictionary* infoDict = [[NSBundle mainBundle] infoDictionary];
    if([infoDict objectForKey:@"ttdebug"]) {
        return [[infoDict objectForKey:@"ttdebug"] boolValue];
    }
    return NO;
}
@end

#pragma mark - Unity Bridge
extern "C" {
    bool _IsTTDebug() {
        return [TTDebug IsDebug];
    }
}
