import { browser, by, user } from 'protractor';

export class AppPage {
  navigateTo() {
    return browser.get(browser.baseUrl) as Promise<any>;
  }

  getTitleText() {
    return user(by.css('app-root h1')).getText() as Promise<string>;
  }
}
