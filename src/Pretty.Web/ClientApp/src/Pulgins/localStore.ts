
export default {
  setJSON(key: any, value: any) {
    this.set(key, JSON.stringify(value));
  },
  set(key: any, value: any) {
    localStorage.setItem(key, value);
  },
  getJSON(key: any) {
    return this.get(key) ? JSON.parse(this.get(key) || '{}') : null;
  },
  get(key: any) {
    return localStorage.getItem(key);
  }
}