/**
 * A class containing storage functions
 */
export class StorageUtils {
  /**
   * A function that saves the user to the local storage
   *
   * @param key The key found at consts/storage-keys.enum.ts file
   * @param value The value that is getting saved
   */
  public static storageSave<T>(key: string, value: T): void {
    localStorage.setItem(key, JSON.stringify(value));
  }
  /**
   *
   * A function that reads the value from the local storage
   *
   * @param key The key found at consts/storage-keys.enum.ts file
   * @returns undefined if value doesn't exist
   */

  public static storageRead<T>(key: string): T | undefined {
    const storedValue = localStorage.getItem(key);
    try {
      if (storedValue) return JSON.parse(storedValue) as T;

      return undefined;
    } catch (e) {
      localStorage.removeItem(key);
      return undefined;
    }
  }

  /**
   *
   * A function that removes the user in local storage
   *
   * @param key The key found at consts/storage-keys.enum.ts file
   */

  public static storageDelete(key: string) {
    localStorage.removeItem(key);
  }
}
