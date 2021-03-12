#import "UnityAppController.h"
#include "AppDelegateListener.h"

@interface CustomUnityAppController : UnityAppController
@end

@implementation CustomUnityAppController

- (BOOL)application:(UIApplication *)app openURL:(NSURL *)url options:(NSDictionary<UIApplicationOpenURLOptionsKey,id> *)options
{
    UnitySendMessage("UnityDeeplinks", "onDeeplink", [[url absoluteString] UTF8String]);
    return [super application:app openURL:url options:options];
}

@end

IMPL_APP_CONTROLLER_SUBCLASS(CustomUnityAppController)
