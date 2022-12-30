export interface Storage {
  getValueFromStorage: (key: string) => string;
  setValueInstorage: (key: string, initialValue: string) => void;
  clearStorage: (key: string) => void;
}

export const useStorage = (): Storage => {
  const getValueFromStorage = (key: string): string => {
    return localStorage.getItem(key) ?? '';
  };

  const setValueInstorage = (key: string, initialValue: string): void => {
    const result = localStorage.getItem(key);
    localStorage.setItem(key, initialValue);
  };

  const clearStorage = (key: string): void => {
    localStorage.removeItem(key);
  };

  const storage: Storage = {
    getValueFromStorage,
    setValueInstorage,
    clearStorage,
  };

  return storage;
};
