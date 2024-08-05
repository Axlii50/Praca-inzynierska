import styles from "./Prompt.module.css";

import { IoIosArrowDroprightCircle } from "react-icons/io";

function Prompt() {
  return (
    <form className={styles.prompt}>
      <input
        type="text"
        className={styles.insert}
        placeholder="Insert to modify your image"
      />
      <button className={styles.iconBtn}>
        <IoIosArrowDroprightCircle size={48} />
      </button>
    </form>
  );
}

export default Prompt;
