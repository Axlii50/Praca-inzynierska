import Header from "./components/header/Header";
import Main from "./components/main/Main";
import styles from "./Layout.module.css";

function Layout() {
  return (
    <div className={styles.layout}>
      <Header />
      <Main />
    </div>
  );
}

export default Layout;
